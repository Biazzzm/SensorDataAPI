using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SensorData.Data;
using SensorData.Models;
using SensorDataAPI.ViewModels;
using Telegram.Bot.Types;

namespace SensorDataAPI.Controllers
{
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly SensorDBcontext _context;

        public ContactController(SensorDBcontext context)
        { 
            _context = context;
        }

        [HttpGet]
        [Route("v1/api/contacts/{userId}")]

        public async Task<IActionResult> GetAsync(
            [FromRoute] int userId,
            [FromServices] SensorDBcontext _context)
        {
            try
            {
                var contatoExistente = _context.Contacts.FirstOrDefault(c =>c.UserId == userId);
                var contact = await _context.Contacts.ToListAsync();

                return Ok(contact);
            }

            catch (Exception ex)
            {
                return BadRequest("Não foi possível encontrar os contatos de emergência. Erro: " + ex.Message);
            }

        }



        [HttpPost]
        [Route("v1/api/contacts")]
        public async Task<IActionResult> PostAsync([FromBody] ContactViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var contact = new ContactModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    ChatId = model.ChatId,
                    UserId = model.UserId
                };

                _context.Contacts.Add(contact);
                await _context.SaveChangesAsync();

                return Ok(contact);
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao cadastrar contato: {ex.Message}");
            }
        }


        [HttpPut]
        [Route("v1/api/contacts/{userId}/{Id}")]

        public async Task<IActionResult> PutAsync(
            [FromRoute] int Id,
            [FromRoute] int userId,
            [FromBody] ContactViewModel model)
        {
            try
            {
                var usuarioExistente = await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == Id);

                if (usuarioExistente == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                usuarioExistente.Name = model.Name;
                usuarioExistente.Email = model.Email;
                usuarioExistente.ChatId = model.ChatId;

                _context.Contacts.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                return Ok(usuarioExistente);
            }


            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o usuário. Erro: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("v1/api/contacts/{userId}/{Id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int Id,
            [FromRoute] int userId)
        {
            var contatoExistente = await _context.Contacts.FirstOrDefaultAsync(c => c.UserId == userId && c.Id == Id);


            if (contatoExistente == null)
            {
                return NotFound("contato não encontrado");
            }

            _context.Contacts.Remove(contatoExistente);
            await _context.SaveChangesAsync();


            return Ok(contatoExistente);

        }

    }
}
