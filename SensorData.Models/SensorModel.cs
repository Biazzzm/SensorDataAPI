﻿using System.Text.Json.Serialization;

namespace SensorData.Models
{
    public class SensorModel
    {
        public int Id { get; set; }
        public int SensorValue { get; set; }
        public int UserId { get; set; }
        public DateTime Timestamp { get; set; }

        [JsonIgnore]
        public UserModel? User { get; set; }

        // Adicionando a propriedade para diferenciar MQ-2 e MQ-4
        public string SensorType { get; set; }
    }
}
