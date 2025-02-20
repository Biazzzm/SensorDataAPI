using Newtonsoft.Json.Linq;
using Microsoft.Extensions.Hosting;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;


namespace SensorDataAPI.Services
{
    public class TelegramPollingService : BackgroundService
    {
        private readonly TelegramService _telegramservice;
        private readonly string _bottoken = "7810065799:aagmfaru101wuwj6m8cpmj_0hdhcfv-psk8";

        public TelegramPollingService(TelegramService telegramservice)
        {
            _telegramservice = telegramservice;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingtoken)
        {
            int lastupdateid = 0;

            while (!stoppingtoken.IsCancellationRequested)
            {
                using HttpClient client = new HttpClient();
                var url = $"https://api.telegram.org/bot{_bottoken}/getupdates?offset={lastupdateid + 1}";
                var response = await client.GetStringAsync(url);

                var updates = JObject.Parse(response)["result"];

                foreach (var update in updates)
                {
                    var message = update["message"];
                    if (message != null)
                    {
                        long chatid = message["chat"]["id"].Value<long>();
                        string text = message["text"].Value<string>();

                        // exibe o chatid no console para facilitar o teste
                        Console.WriteLine($"novo chatid capturado: {chatid}");

                        // chama o método para tratar o comando
                        await _telegramservice.HandleCommandAsync(text, chatid);
                    }

                    lastupdateid = update["update_id"].Value<int>();
                }

                await Task.Delay(1000); // evita sobrecarregar o servidor
            }
        }
    }
}


