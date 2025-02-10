using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorData.Data;

namespace SensorDataAPI.Controllers
{
    [ApiController]
    public class AlertaController : ControllerBase
    {
        [HttpGet]
        [Route("v1/api/alerts/{userId}")]

        public async Task<IActionResult> GetAsync(
            [FromRoute] int userId,
            [FromServices] SensorDBcontext _context)
        {
            try
            {
                // Busca todos os alertas para o userId
                var alertas = await _context.Alertas
                                            .Where(a => a.UserId == userId)
                                            .ToListAsync();

                // Verifica se foram encontrados alertas
                if (alertas == null || !alertas.Any())
                {
                    return NotFound(new { message = "Nenhum alerta encontrado para este usuário." });
                }

                return Ok(alertas); // Retorna os alertas encontrados no banco
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Não foi possível encontrar os alertas. Erro: " + ex.Message });
            }
        }
    }
}
