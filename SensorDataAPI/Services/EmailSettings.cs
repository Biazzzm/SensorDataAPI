namespace SensorDataAPI.Services
{
    public class EmailSettings
    {
        public string SendGridApiKey { get; set; } // Chave de API do SendGrid
        public string FromEmail { get; set; }      // E-mail do remetente
        public string FromName { get; set; }       // Nome do remetente
    }
}