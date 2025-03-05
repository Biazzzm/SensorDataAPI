using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorData.Data;
using SensorData.Models;
using SensorDataAPI.Services;
using Telegram.Bot.Types;

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
        public async Task<IActionResult> Post([FromRoute] string email, [FromBody] SensorModel data, [FromServices] SensorDBcontext _context, [FromServices] EmailService _emailService)
        {
            try
            {
                // Carrega o usuário e seus contatos de emergência
                var user = await _context.Users
                    .Include(u => u.ContactsList) // Carrega os contatos de emergência
                    .FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return BadRequest("Usuário não encontrado com esse e-mail.");
                }

                int userId = user.Id;
                data.User = user;
                data.UserId = userId;

                if (data.SensorValue > 400)
                {
                    if (DateTime.Now - _lastAlertTime > CooldownPeriod)
                    {
                        var alerta = new AlertaModel
                        {
                            SensorValue = data.SensorValue,
                            DataHora = DateTime.UtcNow,
                            UserId = userId
                        };

                        _context.Alertas.Add(alerta);
                        await _context.SaveChangesAsync();

                        var message = @$"🚨 Alerta de Gás ou Fumaça Detectada!

Olá {user.Name}, 

Detectamos um nível de gás ou fumaça em sua área. Sua segurança é nossa prioridade!

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

                        // Prepara a lista de usuários e contatos
                        var users = new List<UserModel> { user };
                        var contacts = user.ContactsList?.ToList() ?? new List<ContactModel>();

                        // Envia o e-mail para todos os destinatários
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
                    return Ok(new { status = "Valor do sensor não crítico, dados não salvos." });
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao processar dados: " + ex.Message);
            }
        }
    }
}

