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
        public SensorDataController(SensorDBcontext context)
        {
            _telegramService = new TelegramService();
            _context = context;
        }

        [HttpGet]
        [Route("api/SensorData/test")]
        public async Task<IActionResult> Get()
        {
            return Ok("Ok");
        }

        [HttpPost]
        [Route("api/SensorData/post-sensor-data/{email}")]
        public async Task<IActionResult> Post([FromRoute] string email, [FromBody] SensorModel data, [FromServices] SensorDBcontext _context)
        {
            try
            {
                // Encontre o userId com base no e-mail
                var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

                if (user == null)
                {
                    return BadRequest("Usuário não encontrado com esse e-mail.");
                }

                // Agora você tem o userId
                int userId = user.Id;

                // Atribuindo o objeto do usuário no SensorModel
                data.User = user;
                data.UserId = userId; // O UserId ainda é necessário para relacionar com outras tabelas, como Alertas

                // Verifica se o valor do sensor é maior que 400 (indicando presença de fumaça)
                if (data.SensorValue > 400)
                {
                    // Verifica se já passou o tempo de cooldown ou se nunca enviou alerta
                    if (DateTime.Now - _lastAlertTime > CooldownPeriod)
                    {
                        // Cria um alerta para esse sensor
                        var alerta = new AlertaModel
                        {
                            SensorValue = data.SensorValue,
                            DataHora = DateTime.UtcNow,
                            UserId = userId
                        };

                        // Salva o alerta no banco de dados
                        _context.Alertas.Add(alerta);
                        await _context.SaveChangesAsync();

                        // Mensagem de alerta para o Telegram
                        var message = @$"🚨 Alerta de Gás ou Fumaça Detectada! 🚨

Detectamos um nível de gás ou fumaça em sua área. Sua segurança é nossa prioridade!

Deseja ligar para os serviços de emergência? Aqui estão os números:

Polícia: 190  
Bombeiros: 193  

Por favor, se sentir que está em risco, entre em contato imediatamente com os serviços de emergência.

Fique seguro(a)!";

                        // Envia a mensagem para o Telegram
                        await _telegramService.SendMessage(message);

                        // Atualiza o tempo do último alerta enviado
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


