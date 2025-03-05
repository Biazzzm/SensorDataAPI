using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorData.Data;
using SensorData.Models;
using SensorDataAPI.Services;

namespace SensorDataAPI.Controllers
{
    [ApiController]

    public class SensorDataController : ControllerBase
    {
        private readonly TelegramService _telegramService;
        private static DateTime _lastAlertTime = DateTime.MinValue; // Variável para controlar o tempo do último alerta
        private static readonly TimeSpan CooldownPeriod = TimeSpan.FromMinutes(5); // Período de cooldown para novos alertas
        private readonly SensorDBcontext _context;
        private readonly EmailService _emailService;
        private readonly ILogger<SensorDataController> _logger;
        public SensorDataController(SensorDBcontext context, EmailService emailService, ILogger<SensorDataController> logger)
        {
            _telegramService = new TelegramService();
            _context = context;
            _emailService = emailService;
            _logger = logger;
        }

        [HttpGet]
        [Route("api/SensorData/test")]
        public async Task<IActionResult> Get()
        {
            return Ok("Ok");
        }

        [HttpPost]
        [Route("api/sensordata/post-sensor-data/{email}")]
        public async Task<IActionResult> Post(
    [FromRoute] string email,
    [FromBody] SensorModel data,
    [FromServices] SensorDBcontext _context,
    [FromServices] EmailService _emailService)
        {
            try
            {
                var user = await _context.Users
                    .Include(u => u.ContactsList)
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return BadRequest("Usuário não encontrado com esse e-mail.");
                }

                int userId = user.Id;
                data.User = user;
                data.UserId = userId;

                if (string.IsNullOrEmpty(data.SensorType))
                {
                    return BadRequest("O tipo de sensor não foi informado.");
                }


                // Definir os limites para os sensores MQ-2 e MQ-4
                int mq2Threshold = 400;  // Limite para MQ-2
                int mq4Threshold = 400;  // Limite para MQ-4

                bool alertTriggered = false;



                // Verificar o valor do sensor com base no tipo
                if (data.SensorType == "MQ-2" && data.SensorValue > mq2Threshold)
                {
                    alertTriggered = true;
                }
                else if (data.SensorType == "MQ-4" && data.SensorValue > mq4Threshold)
                {
                    alertTriggered = true;
                }

                // Se algum sensor disparou alerta, enviar o alerta
                if (alertTriggered)
                {
                    // Verificar se o tempo de cooldown foi respeitado
                    if (DateTime.Now - _lastAlertTime > CooldownPeriod)
                    {
                        var alerta = new AlertaModel
                        {
                            SensorValue = data.SensorValue,
                            DataHora = DateTime.UtcNow,
                            UserId = user.Id,
                            SensorType = data.SensorType // Armazenando qual sensor disparou o alerta
                        };

                        _context.Alertas.Add(alerta);
                        await _context.SaveChangesAsync();

                        var message = @$"🚨 Alerta de {data.SensorType} Detectado!

Nível crítico detectado: {data.SensorValue}.
                        
Olá {user.Name}, Detectamos um nível de gás ou fumaça em sua área. Sua segurança é nossa prioridade!

Deseja ligar para os serviços de emergência? Aqui estão os números:

Polícia: 190
Bombeiros: 193

Por favor, se sentir que está em risco, entre em contato imediatamente com os serviços de emergência.

Fique seguro(a)!";

                        var chatIds = new List<string> { user.ChatId };
                        if (user.ContactsList != null)
                        {
                            chatIds.AddRange(user.ContactsList.Where(c => !string.IsNullOrEmpty(c.ChatId)).Select(c => c.ChatId));
                        }

                        await _telegramService.SendAlertMessageAsync(message, chatIds);

                        var users = new List<UserModel> { user };
                        var contacts = user.ContactsList?.ToList() ?? new List<ContactModel>();

                        await _emailService.SendEmailAsync(users, contacts, "🚨 Alerta de Gás ou Fumaça Detectada!", message);

                        _lastAlertTime = DateTime.Now;

                        return Ok(new { status = "Alerta enviado e dados salvos com sucesso." });
                    }
                    else
                    {
                        return Ok(new { status = "Alerta já enviado recentemente, aguardando cooldown." });
                    }
                }
                else
                {
                    return Ok(new { status = "Nenhum valor crítico detectado. Dados não salvos." });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao receber a leitura de gas");
                return BadRequest("Erro ao processar dados: " + ex.Message);
            }
        }

    }
}

