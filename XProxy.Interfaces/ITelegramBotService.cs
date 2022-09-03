using XProxy.Domain;

namespace XProxy.Interfaces;

public interface ITelegramBotService
{
    Task SendMessageDefault(long chatId, string message, CancellationToken token);
    Task SendMessage(long userSettingsId, long chatId, string message, CancellationToken token);

    Task SendMessageToAdmin(long userSettingsId, string message, CancellationToken token);
    Task SendMessageToAdminDefault(string message, CancellationToken token);
}