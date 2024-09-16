using System;
using AnnouncementProcessor.ViewModels;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using AnnouncementProcessor.Managers;

namespace AnnouncementProcessor.Proxies
{
    public class TelegramBotMessageProxy : ITelegramBotMessageProxy
    {
        private readonly ITelegramBotClient _telegramBotClient;
        private readonly IInputFileManager _inputFileManager;
        private readonly IBlobServiceManager _blobServiceManager;

        public TelegramBotMessageProxy(
            ITelegramBotClient telegramBotClient,
            IInputFileManager inputFileManager)
            
        {
            _telegramBotClient = telegramBotClient;
            _inputFileManager = inputFileManager;
        }

        public async Task<Message> SendAnnouncement(AnnouncementMessage announcement) => announcement.MessageType switch
        {
            MessageType.Text => await _telegramBotClient.SendTextMessageAsync(announcement.ChatId, text: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Photo => await _telegramBotClient.SendPhotoAsync(announcement.ChatId, photo: await _inputFileManager.GetInputFile(announcement), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Video => await _telegramBotClient.SendVideoAsync(announcement.ChatId, video: await _inputFileManager.GetInputFile(announcement), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup, height: announcement.ContentProperties?.Height, width: announcement.ContentProperties?.Width, supportsStreaming: announcement.ContentProperties?.SupportStreaming),
            MessageType.VideoNote => await _telegramBotClient.SendVideoNoteAsync(announcement.ChatId, videoNote: await _inputFileManager.GetInputFile(announcement), replyMarkup: announcement.ReplyMarkup),
            MessageType.Voice => await _telegramBotClient.SendVoiceAsync(announcement.ChatId, voice: await _inputFileManager.GetInputFile(announcement), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Audio => await _telegramBotClient.SendAudioAsync(announcement.ChatId, audio: await _inputFileManager.GetInputFile(announcement), caption: announcement.Text),
            MessageType.Sticker => await _telegramBotClient.SendStickerAsync(announcement.ChatId, await _inputFileManager.GetInputFile(announcement)),
            _ => null
        };
    }
}

