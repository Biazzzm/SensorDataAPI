using SensorData.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SensorDataAPI.ViewModels
{
    public class ContactViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(160, MinimumLength = 3, ErrorMessage = "O nome deve conter entre 3 e 160 caracteres")]
        public string Name { get; set; } = string.Empty;
        //[Required]
        [EmailAddress(ErrorMessage = "Digite um email válido")]
        public string? Email { get; set; }

        public string? ChatId { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }

       
    }
}
