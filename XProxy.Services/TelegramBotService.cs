using Telegram.Bot;
using Telegram.Bot.Types;
using XProxy.Interfaces;

namespace XProxy.Services;

public class TelegramBotService : ITelegramBotService
{
    private readonly IXProxyOptions _xProxyOptions;

    public TelegramBotService(IXProxyOptions xProxyOptions)
    {
        _xProxyOptions = xProxyOptions;
    }

    public async Task SendMessage(long chatId, string message)
    {        
        await (new TelegramBotClient(_xProxyOptions.TelegramBotToken))
            .SendTextMessageAsync(new ChatId(chatId), message);
    }

    public async Task SendMessageToAdmin(string message)
    {
        await SendMessage(_xProxyOptions.TelegramAdminChatId, message);
    }
}