using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SensorData.Models
{
    public class AlertaModel
    {
        //[Key]
        public int Id { get; set; }

        //[Required]
        public int SensorValue { get; set; }
        //[Required]
        //[DataType(DataType.Date)]
        public DateTime DataHora { get; set; }
        //[ForeignKey("UserModel")]
        public int UserId { get; set; }

        //[InverseProperty("AlertasList")]
        public UserModel User { get; set; }

        // Novo campo para armazenar o tipo do sensor (MQ-2 ou MQ-4)
        [Required]
        public string SensorType { get; set; }


    }
}
