// See https://aka.ms/new-console-template for more information
using System.Threading;
using System.Threading.Tasks;

using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Exceptions;

TelegramBot.Main();


public class TelegramBot
{
    static ITelegramBotClient bot = new TelegramBotClient("5507968691:AAFT-N3XPOFU8pZSIto73D9X2Z7lOGpoNvw");
    public static async Task HandleUpdateAsync(ITelegramBotClient botClient,
        Update update, CancellationToken cancellationToken)
    {
        // некоторые действия
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(update));

        // если тип обновления ТЕКСТОВОЕ СООБЩЕНИЕ 
        if(update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
        {
            var message = update.Message;// сообщение в новую переменную
            // если юзер боту отправил /start (нажал кнопку запуск)
            if(message.Text.ToLower() == "/start")
            {
                // ... то отправляем сообщение пользователю
                await botClient.SendTextMessageAsync(message.Chat, "Welcome ^.^");
                // прерываем выполнение метода
                return;
            } 
            // ... в противном, отправляем другое сообщение
            await botClient.SendTextMessageAsync(message.Chat, "Hi!");
        }
    }

    public static async Task HandleErrorAsync(ITelegramBotClient botClient, Exception exception,
        CancellationToken cancellationToken)
    {
        Console.WriteLine(Newtonsoft.Json.JsonConvert.SerializeObject(exception));
    }

    public static void Main()
    {
        Console.WriteLine("Bot is ready..." + bot.GetMeAsync().Result.FirstName);

        var cts = new CancellationTokenSource();
        var cancellationToken = cts.Token;
        var receiverOptions = new ReceiverOptions
        {
            AllowedUpdates = { }, //receive all update types
        };
        bot.StartReceiving(
            HandleUpdateAsync,
            HandleErrorAsync,
            receiverOptions,
            cancellationToken);

        Console.ReadLine();

    }
}
