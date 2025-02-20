using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SecureIdentity.Password;
using SensorData.Data;
using SensorData.Models;
using SensorDataAPI.EditorViewModel;
using SensorDataAPI.ViewModels;


namespace SensorDataAPI.Controllers
{

    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly SensorDBcontext _context;

        public UserController(SensorDBcontext context)
        {
            _context = context;
        }

        [HttpGet]
        [Route("v1/api/users")]

        public async Task<IActionResult> GetAsync([FromServices] SensorDBcontext _context)
        {
            try
            {
                var user = await _context.Users.ToListAsync();

                return Ok(user);
            }

            catch (Exception ex)
            {
                return BadRequest("Não foi possível encontrar os usuários. Erro: " + ex.Message);
            }

        }
        [HttpGet]
        [Route("v1/api/users/{userId}")]
        public IActionResult GetByIdAsync(
            [FromRoute] int userId)
        {
            try
            {
                var user = _context.Users.FirstOrDefault(x => x.Id == userId);

                if (user == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                return Ok(user);
            }

            catch (Exception ex)
            {
                return BadRequest("Não foi possível encontrar os usuários. Erro: " + ex.Message);
            }

        }

        

        [HttpPost]
        [Route("v1/api/users")]
        public async Task<IActionResult> PostAsync(
            [FromBody] UserViewModel model)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {

                var user = new UserModel
                {
                    Name = model.Name,
                    Email = model.Email,
                    Password = PasswordHasher.Hash(model.Password),
                    ChatId = model.ChatId,
                };

                if (_context.Users.Any(u => u.Email == user.Email))
                {
                    return BadRequest("Já existe um usuário com este e-mail.");
                }


                await _context.Users.AddAsync(user);
                await _context.SaveChangesAsync();

                return Ok(user.Name);
            }


            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o usuário. Erro: " + ex.Message);
            }
        }

        [HttpPut]
        [Route("v1/api/users/{userId}")]

        public async Task<IActionResult> PutAsync(
            [FromRoute] int userId,
            [FromBody] UserViewModel model)
        {
            try
            {
                var usuarioExistente = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);

                if (usuarioExistente == null)
                {
                    return NotFound("Usuário não encontrado");
                }

                usuarioExistente.Name = model.Name;
                usuarioExistente.Email = model.Email;
                usuarioExistente.Password = PasswordHasher.Hash(model.Password);

                // Verifica se a senha foi alterada antes de criptografá-la
                if (!string.IsNullOrEmpty(model.Password))
                {
                    usuarioExistente.Password = PasswordHasher.Hash(model.Password);
                }

                usuarioExistente.ChatId = model.ChatId;

                _context.Users.Update(usuarioExistente);
                await _context.SaveChangesAsync();

                return Ok(usuarioExistente);
            }


            catch (Exception ex)
            {
                return BadRequest("Não foi possível cadastrar o usuário. Erro: " + ex.Message);
            }
        }
        [HttpDelete]
        [Route("v1/api/users/{id}")]

        public async Task<IActionResult> DeleteAsync(
            [FromRoute] int id)
        {
            var usuarioExistente = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (usuarioExistente == null)
            {
                return NotFound("Usuário não encontrado");
            }

            _context.Users.Remove(usuarioExistente);
            await _context.SaveChangesAsync();


            return Ok(usuarioExistente);

        }


        [HttpPost]
        [Route("v1/api/users/login")]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest(ModelState);

                var usuario = _context.Users.FirstOrDefault(u => u.Email == model.Email);

                if (usuario == null)
                {
                    return Unauthorized("E-mail ou senha inválidos.");
                }

                bool senhaValida = PasswordHasher.Verify(usuario.Password, model.Password);

                if (!senhaValida)
                {
                    return Unauthorized("E-mail ou senha inválidos");
                }

                return Ok(new { message = "Login realizado com sucesso", userId = usuario.Id });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro no servidor: {ex.Message}");
                return StatusCode(500, "Erro interno do servidor");
            }
        }

    }
}
