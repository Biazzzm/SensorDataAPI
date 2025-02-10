using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Models
{
    public class UserModel
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        //[StringLength(160, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        //[Required]
        //[EmailAddress]
        public string Email { get; set; } = string.Empty;
        //[Required]
        //[StringLength(255)]
        public string Password { get; set; } = string.Empty;
        //[StringLength(20)]
        public string? ChatId { get; set; }

        public List<ContactModel>? ContactsList { get; set; }

        // Lista de alertas do usuário
        public List<AlertaModel>? AlertasList { get; set; } = new List<AlertaModel>();

        // Lista de sensores associados ao usuário
        public SensorModel Sensors { get; set; }
    }
}
