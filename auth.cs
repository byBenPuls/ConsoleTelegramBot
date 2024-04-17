using System;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;


class TelegramBot
{
    int Initialize(string TelegramBotToken)
    {
        var botClient = new TelegramBotClient(TelegramBotToken);
        var me = await botClient.GetMeAsync();

        using CancellationTokenSource cts = new();

        ReceiverOptions receiverOptions = new()
        {
            AllowedUpdates = Array.Empty<UpdateType>() // receive all update types except ChatMember related updates
        };
    }
}