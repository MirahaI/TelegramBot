//using System;

//namespace TelegramBot
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("Hello World!");
//        }
//    }
//}

using System;
using System.Threading;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

var botClient = new TelegramBotClient("5400187579:AAHOVyDLi9jdp6ZN6p8QNTqvUedcSs7Vsa8");

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions
{
    AllowedUpdates = Array.Empty<UpdateType>() // receive all update types
};
botClient.StartReceiving(
    updateHandler: HandleUpdateAsync,
    pollingErrorHandler: HandlePollingErrorAsync,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token
);

var me = await botClient.GetMeAsync();

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();

async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
{
    // Only process Message updates: https://core.telegram.org/bots/api#message
    if (update.Message is not { } message)
        return;
    // Only process text messages
    if (message.Text is not { } messageText)
        return;

    var chatId = message.Chat.Id;

    Console.WriteLine($"Received a '{messageText}' message in chat {chatId}.");
    //Aboba
    Message message1;
    using (var video1 = System.IO.File.OpenRead("C:\\Users\\Admin\\Downloads\\VideoMn2O4.mp4"))
    {
        message1 = await botClient.SendVideoAsync(
        chatId: chatId,
        video: video1,
        thumb: "https://cdn.pixabay.com/photo/2017/02/27/15/39/test-tube-2103510__340.png",
        caption: "Source: https://youtu.be/T0SwsNI8hMQ");
    }

    //Message message1 = await botClient.SendVideoAsync(
    //    chatId: chatId,
    //    video: "https://youtu.be/T0SwsNI8hMQ",
    //    //video: "https://raw.githubusercontent.com/TelegramBots/book/master/src/docs/video-countdown.mp4",
    //    thumb: "https://raw.githubusercontent.com/TelegramBots/book/master/src/2/docs/thumb-clock.jpg",
    //    supportsStreaming: true,
    //    cancellationToken: cancellationToken);

    //Message message1; 
    //using (var stream = System.IO.File.OpenRead("C:\\Users\\Admin\\Downloads\\src_docs_voice-nfl_commentary.ogg"))
    //{
    //    message1 = await botClient.SendVoiceAsync(
    //        chatId: chatId,
    //        voice: stream,
    //        duration: 36,
    //        cancellationToken: cancellationToken);
    //}

    //Message message1 = await botClient.SendAudioAsync(
    //chatId: chatId,
    //audio: "https://github.com/TelegramBots/book/raw/master/src/docs/audio-guitar.mp3",
    ///*
    //performer: "Joel Thomas Hunger",
    //title: "Fun Guitar and Ukulele",
    //duration: 91, // in seconds
    //*/
    //cancellationToken: cancellationToken);

    //Message message1 = await botClient.SendStickerAsync(
    //chatId: chatId,
    //sticker: "https://github.com/TelegramBots/book/raw/master/src/docs/sticker-fred.webp",
    //cancellationToken: cancellationToken);

    //Message message2 = await botClient.SendStickerAsync(
    //    chatId: chatId,
    //    sticker: message1.Sticker.FileId,
    //    cancellationToken: cancellationToken);


    //Message message1 = await botClient.SendPhotoAsync(
    //chatId: chatId,
    //photo: "https://github.com/TelegramBots/book/raw/master/src/docs/photo-ara.jpg",
    //caption: "<b>Ara bird</b>. <i>Source</i>: <a href=\"https://pixabay.com\">Pixabay</a>",
    //parseMode: ParseMode.Html,
    //cancellationToken: cancellationToken);
    //// Echo received message text
    //Message sentMessage = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "You said:\n" + messageText,
    //    cancellationToken: cancellationToken);


    //if (messageText == "Send sticker")
    //{
    //    Message message1 = await botClient.SendStickerAsync(
    //                chatId: chatId,
    //                sticker: ("https://tgram.ru/wiki/stickers/img/persik_animated/gif/1.gif"),
    //                cancellationToken: cancellationToken);
    //}
    //if (messageText == "Send photo")
    //{
    //    Message message1 = await botClient.SendPhotoAsync(
    //        chatId: chatId,
    //        photo: ("https://img.freepik.com/free-vector/realistic-light-bulb-with-electricity_23-2149129410.jpg?w=2000"),
    //        cancellationToken: cancellationToken);
    //}
    //else
    //{
    //    Message message1 = await botClient.SendTextMessageAsync(
    //    chatId: chatId,
    //    text: "Hello World",
    //    cancellationToken: cancellationToken);
    //}

    //Console.WriteLine(
    //$"{message.From.FirstName} sent message {message.MessageId} " +
    //$"to chat {message.Chat.Id} at {message.Date}. " +
    //$"It is a reply to message {message.ReplyToMessage.MessageId} " +
    //$"and has {message.Entities.Length} message entities."
    //);

    //Message message1 = await botClient.SendTextMessageAsync(
    //chatId: chatId,
    //text: "Trying *all the parameters* of `sendMessage` method",
    //parseMode: ParseMode.MarkdownV2,
    //disableNotification: true,
    //replyToMessageId: update.Message.MessageId,
    //replyMarkup: new InlineKeyboardMarkup(
    //    InlineKeyboardButton.WithUrl(
    //        "Check sendMessage method",
    //        "https://core.telegram.org/bots/api#sendmessage")),
    //cancellationToken: cancellationToken);
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