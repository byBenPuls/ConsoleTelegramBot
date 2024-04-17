using System;
using System.Runtime.InteropServices;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

Console.ForegroundColor = ConsoleColor.Blue;
Console.WriteLine("PROGRAM CREATED BY BEN PULS! \n");
Console.ResetColor();
Console.Write("WRITE YOUR TELEGRAM BOT TOKEN: ");
string TelegramBotToken = Console.ReadLine();
Console.Clear();

var botClient = new TelegramBotClient(TelegramBotToken);
var me = await botClient.GetMeAsync();

using CancellationTokenSource cts = new();


    Console.ForegroundColor = ConsoleColor.Yellow;
    Console.WriteLine($"{me.FirstName} [{me.Id}] successfully logged!");
    Console.ResetColor();


botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    cancellationToken: cts.Token
);


async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{

    if (update.Message is not { } message)
        return;
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;
    var userName = $"{message.Chat.FirstName} {message.Chat.LastName}";

    Console.WriteLine($"Received '{messageText}' from {userName} [{chatId}].");

    /* For answer to message
    Message sentMessage = await botClient.SendTextMessageAsync(
        chatId: chatId,
        text: "You said:\n" + messageText,
        cancellationToken: cancellationToken);
    */
}

Task HandlePollingErrorAsync(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
{
    var ErrorMessage = exception switch
    {
        ApiRequestException apiRequestException
            => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
        _ => exception.ToString()
    };

    Console.WriteLine(ErrorMessage);
    return Task.CompletedTask;
}

Console.ReadKey();

return 0;
