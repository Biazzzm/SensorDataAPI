using Microsoft.AspNetCore.Mvc;
using SensorDataAPI.Services;
using System.Threading.Tasks;

namespace SensorDataAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly TelegramService _telegramService;

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
            // Verifica se o valor lido é maior que 300 (indicando presença de fumaça)
            if (data.SensorValue > 400)
            {
                var message = @$"🚨 Alerta de Gás ou Fumaça Detectada! 🚨

Detectamos um nível de gás ou fumaça em sua área. Sua segurança é nossa prioridade!

Deseja ligar para os serviços de emergência? Aqui estão os números:

Polícia: 190
Bombeiros: 193

Por favor, se sentir que está em risco, entre em contato imediatamente com os serviços de emergência.

Fique seguro(a)!";

                await _telegramService.SendMessage(message);
                return Ok(new { status = "Mensagem enviada com sucesso" });

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

