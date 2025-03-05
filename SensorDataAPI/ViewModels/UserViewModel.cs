using SensorData.Models;
using System.ComponentModel.DataAnnotations;

namespace SensorDataAPI.EditorViewModel
{
    public class UserViewModel
    {
        public int Id { get; set; } 

        [Required (ErrorMessage = "O nome é obrigatório!")]
        [StringLength(160, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 160 caracteres")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "O e-mail é obrigatório!")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é válido!")]
        public string Email { get; set; } = string.Empty;
        [Required(ErrorMessage = "A senha é obrigatória!")]
        [StringLength(255)]
        public string Password { get; set; } = string.Empty;

        public string? ChatId { get; set; } = null;

        
    }
}
