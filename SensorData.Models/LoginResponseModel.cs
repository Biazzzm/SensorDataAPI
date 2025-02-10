using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SensorData.Models
{
    public class LoginResponseModel
    {
        [JsonPropertyName("UserId")]
        public int UserId { get; set; }
        public string Nome { get; set; }
    }
}

