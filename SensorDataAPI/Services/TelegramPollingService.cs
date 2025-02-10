using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace SensorDataAPI.Services
{
    public class TelegramPollingService : BackgroundService
    {
        private readonly TelegramService _telegramService;
        private readonly string _botToken = "7810065799:AAGmfARU101WuwJ6M8CPmJ_0hdhcfv-psK8";

        public TelegramPollingService(TelegramService telegramService)
        {
            _telegramService = telegramService;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            int lastUpdateId = 0;

            while (!stoppingToken.IsCancellationRequested)
            {
                using HttpClient client = new HttpClient();
                var url = $"https://api.telegram.org/bot{_botToken}/getUpdates?offset={lastUpdateId + 1}";
                var response = await client.GetStringAsync(url);

                var updates = JObject.Parse(response)["result"];

                foreach (var update in updates)
                {
                    var message = update["message"];
                    if (message != null)
                    {
                        long chatId = message["chat"]["id"].Value<long>();
                        string text = message["text"].Value<string>();

                        // Exibe o chatId no console para facilitar o teste
                        Console.WriteLine($"Novo chatId capturado: {chatId}");

                        // Chama o método para tratar o comando
                        await _telegramService.HandleCommandAsync(text, chatId);
                    }

                    lastUpdateId = update["update_id"].Value<int>();
                }

                await Task.Delay(1000); // Evita sobrecarregar o servidor
            }
        }
    }
}
