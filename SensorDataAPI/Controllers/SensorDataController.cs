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
        public SensorDataController(SensorDBcontext context, EmailService emailService)
        {
            _telegramService = new TelegramService();
            _context = context;
            _emailService = emailService;
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

                if (string.IsNullOrEmpty(data.SensorType))
                {
                    return BadRequest("O tipo de sensor não foi informado.");
                }

                if (data.SensorType != "MQ-2" && data.SensorType != "MQ-4")
                {
                    return BadRequest("Tipo de sensor inválido.");
                }

                data.User = user;
                data.UserId = user.Id;

                // Verifica o tipo de sensor e aplica a condição de valor do sensor
                int sensorThreshold = 0;

                if (data.SensorType == "MQ-2")
                {
                    sensorThreshold = 400; // Limite para MQ-2
                }
                else if (data.SensorType == "MQ-4")
                {
                    sensorThreshold = 300; // Limite para MQ-4
                }

                if (data.SensorValue > sensorThreshold)
                {
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

Olá {user.Name}, 

Detectamos um nível de gás ou fumaça em sua área pelo sensor {data.SensorType}. Sua segurança é nossa prioridade!

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

                        return Ok(new { status = $"Alerta do {data.SensorType} enviado e dados salvos com sucesso." });
                    }
                    else
                    {
                        return Ok(new { status = "Alerta já enviado recentemente, aguardando cooldown." });
                    }
                }
                else
                {
                    return Ok(new { status = $"Valor do {data.SensorType} não crítico, dados não salvos." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao processar dados: " + ex.Message);
            }
        }
    }
}

