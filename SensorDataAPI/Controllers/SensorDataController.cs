using Microsoft.AspNetCore.Mvc;
using SensorDataAPI.Services;
using System;
using System.Threading.Tasks;

namespace SensorDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly TelegramService _telegramService;
        private static DateTime _lastAlertTime = DateTime.MinValue; // Variável para controlar o tempo do último alerta
        private static readonly TimeSpan CooldownPeriod = TimeSpan.FromMinutes(5); // Período de cooldown para novos alertas

        public SensorDataController()
        {
            _telegramService = new TelegramService();
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Ok");
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SensorData data)
        {
            // Verifica se o valor lido é maior que 400 (indicando presença de fumaça)
            if (data.SensorValue > 400)
            {
                // Verifica se já passou o tempo de cooldown ou se nunca enviou alerta
                if (DateTime.Now - _lastAlertTime > CooldownPeriod)
                {
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

                    return Ok(new { status = "Mensagem enviada com sucesso" });
                }
                else
                {
                    return Ok(new { status = "Alerta já enviado recentemente, aguardando cooldown." });
                }
            }
            else
            {
                return Ok(new { status = "Ta Safe" });
            }
        }
    }

    public class SensorData
    {
        public int SensorValue { get; set; }
    }
}


