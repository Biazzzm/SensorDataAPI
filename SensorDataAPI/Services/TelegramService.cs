using Telegram.Bot.Types;
using Telegram.Bot;

namespace SensorDataAPI.Services
{
    public class TelegramService
    {
        private readonly string _telegramBotToken = "7810065799:AAGmfARU101WuwJ6M8CPmJ_0hdhcfv-psK8";
        private readonly string _chatId = "7176432442"; // Pode ser um grupo ou chat privado

        private readonly TelegramBotClient _botClient;

        public TelegramService()
        {
            _botClient = new TelegramBotClient(_telegramBotToken);
        }

        public async Task SendMessage(string message)
        {
            await _botClient.SendMessage(new ChatId(_chatId), message);
        }
    }

}

