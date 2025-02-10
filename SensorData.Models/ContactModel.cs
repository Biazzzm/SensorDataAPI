using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Models
{
    public class ContactModel
    {
        //[Key]
        public int Id { get; set; }
        //[Required]
        //[StringLength(160, MinimumLength = 3)]
        public string Name { get; set; } = string.Empty;
        //[EmailAddress]
        public string? Email { get; set; }
        //[StringLength(20)]
        public string? ChatId { get; set; }

        //[ForeignKey("User")]
        public int UserId { get; set; }

        public UserModel? User { get; set; }

        
    }
}
