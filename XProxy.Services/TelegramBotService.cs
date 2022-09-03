using Telegram.Bot;
using Telegram.Bot.Types;
using XProxy.Domain;
using XProxy.Interfaces;

namespace XProxy.Services;

public class TelegramBotService : ITelegramBotService
{
    private readonly IXProxyOptions _xProxyOptions;
    private readonly IUserSettingsStorage _userSettingsStorage;

    public TelegramBotService(IXProxyOptions xProxyOptions, IUserSettingsStorage userSettingsStorage)
    {
        _xProxyOptions = xProxyOptions;
        _userSettingsStorage = userSettingsStorage;
    }

    public async Task SendMessage(long userSettingsId, long chatId, string message, CancellationToken token)
    {
        var userSettings = await _userSettingsStorage.GetUserSettingsAsync(userSettingsId, token);
        await InternalSend(userSettings.TelegramBotToken, chatId, message);
    }

    public async Task SendMessageDefault(long chatId, string message, CancellationToken token)
    {
        var userSettings = await _userSettingsStorage.GetUserSettingsAsync(_xProxyOptions.DefaultUserSettingsId, token);
        await InternalSend(userSettings.TelegramBotToken, chatId, message);
    }

    public async Task SendMessageToAdmin(long userSettingsId, string message, CancellationToken token)
    {
        var userSettings = await _userSettingsStorage.GetUserSettingsAsync(userSettingsId, token);
        await InternalSend(userSettings.TelegramBotToken, userSettings.TelegramAdminChatId, message);
    }

    public async Task SendMessageToAdminDefault(string message, CancellationToken token)
    {
        var userSettings = await _userSettingsStorage.GetUserSettingsAsync(_xProxyOptions.DefaultUserSettingsId, token);
        await InternalSend(userSettings.TelegramBotToken, userSettings.TelegramAdminChatId, message);

    }

    private async Task InternalSend(string telegramBotToken, long telegramChatId, string message)
    {
        await (new TelegramBotClient(telegramBotToken))
            .SendTextMessageAsync(new ChatId(telegramChatId), message);
    }
}