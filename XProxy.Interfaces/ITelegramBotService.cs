using XProxy.Domain;

namespace XProxy.Interfaces;

public interface ITelegramBotService
{
    Task SendMessage(long chatId, string message);

    Task SendMessageToAdmin(string message);
}