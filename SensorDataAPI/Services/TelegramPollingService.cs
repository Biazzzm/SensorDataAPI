using Newtonsoft.Json.Linq;
using System.Text.Json;


namespace SensorDataAPI.Services
{
    public class TelegramPollingService : BackgroundService
    {
        private readonly TelegramService _telegramService;
        private readonly string _botToken = "7810065799:AAGmfARU101WuwJ6M8CPmJ_0hdhcfv-psK8";

        public TelegramPollingService(TelegramService telegramservice)
        {
            _telegramService = telegramservice;
        }

        public class Update
        {
            public int UpdateId { get; set; }
            public Message Message { get; set; }
        }

        public class Message
        {
            public Chat Chat { get; set; }
            public string Text { get; set; }
        }

        public class Chat
        {
            public long Id { get; set; }
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingtoken)
        {
            try
            {
                int lastupdateid = 0;

                while (!stoppingtoken.IsCancellationRequested)
                {

                    using HttpClient client = new HttpClient();
                    var url = $"https://api.telegram.org/bot{_botToken}/getupdates?offset={lastupdateid + 1}";

                    var response = await client.GetStringAsync(url);

                    var updatesResponse = JsonSerializer.Deserialize<JsonElement>(response);

                    var updates = updatesResponse.GetProperty("result").EnumerateArray();

                    foreach (var update in updates)
                    {
                        var message = update.GetProperty("message");
                        if (message.ValueKind != JsonValueKind.Null)
                        {
                            long chatId = message.GetProperty("chat").GetProperty("id").GetInt64();
                            string text = message.GetProperty("text").GetString();

                            // Exibe o chatId no console para facilitar o teste
                            Console.WriteLine($"Novo chatId capturado: {chatId}");

                            // Chama o método para tratar o comando
                            await _telegramService.HandleCommandAsync(text, chatId);
                        }

                        lastupdateid = update.GetProperty("update_id").GetInt32();
                    }

                    await Task.Delay(1000); // evita sobrecarregar o servidor
                }

            }
            catch (Exception ex)
            {

            }
            
        }
    }
}


