using Telegram.Bot.Types;
using Telegram.Bot;

namespace SensorDataAPI.Services
{
    public class TelegramService
    {
        private readonly string _botToken = "7810065799:AAGmfARU101WuwJ6M8CPmJ_0hdhcfv-psK8";
        

        private readonly TelegramBotClient _botClient;

        public TelegramService()
        {
            _botClient = new TelegramBotClient(_botToken);
        }

        // Método para tratar comandos recebidos no bot
        public async Task HandleCommandAsync(string messageText, long chatId)
        {
            if (messageText == "/getid")
            {
                await SendMessageAsync(chatId, $"Seu chatId é: {chatId}");
            }
            else
            {
                await SendMessageAsync(chatId, "Comando não reconhecido. Tente usar /getid.");
            }
        }

        // Método para enviar uma mensagem para um chatId específico
        public async Task SendMessageAsync(long chatId, string message)
        {
            using HttpClient client = new HttpClient();
            var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";
            await client.GetAsync(url);
        }

        // Método para enviar alerta para vários chatIds
        public async Task SendAlertMessageAsync(string message, List<string> chatIds)
        {
            using HttpClient client = new HttpClient();
            foreach (var chatId in chatIds)
            {
                if (!string.IsNullOrEmpty(chatId))
                {
                    var url = $"https://api.telegram.org/bot{_botToken}/sendMessage?chat_id={chatId}&text={Uri.EscapeDataString(message)}";
                    await client.GetAsync(url);
                }
            }
        }
    }
}
