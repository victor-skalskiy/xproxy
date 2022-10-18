using XProxy.Interfaces;

namespace XProxy.Web;

//todo refac to service
public static class XProxyLogger
{
    public static async void ExceptionLog(string callerName, Exception ex, ITelegramBotService telegramBotService, CancellationToken token)
    {
        await telegramBotService.SendMessageToAdminDefault(
            $"{callerName} Exception: {ex.Message}\r\nInnerException.Message: {ex.InnerException?.Message}\r\nStackTrace: {ex.StackTrace}",
            token);
    }

    public static async void Log(string callerName, string message, ITelegramBotService telegramBotService, CancellationToken token)
    {
        await telegramBotService.SendMessageToAdminDefault(message, token);
    }
}