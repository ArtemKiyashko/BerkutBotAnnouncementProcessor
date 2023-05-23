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
        private readonly IBlobServiceManager _blobServiceManager;

        public TelegramBotMessageProxy(
            ITelegramBotClient telegramBotClient,
            IBlobServiceManager blobServiceManager)
        {
            _telegramBotClient = telegramBotClient;
            _blobServiceManager = blobServiceManager;
        }

        public async Task<Message> SendAnnouncement(AnnouncementMessage announcement) => announcement.MessageType switch
        {
            MessageType.Text => await _telegramBotClient.SendTextMessageAsync(announcement.ChatId, text: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Photo => await _telegramBotClient.SendPhotoAsync(announcement.ChatId, photo: InputFile.FromUri(announcement.ContentUrl), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Video => await _telegramBotClient.SendVideoAsync(announcement.ChatId, video: InputFile.FromUri(announcement.ContentUrl), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.VideoNote => await _telegramBotClient.SendVideoNoteAsync(announcement.ChatId, videoNote: InputFile.FromUri(announcement.ContentUrl), replyMarkup: announcement.ReplyMarkup),
            MessageType.Voice => await _telegramBotClient.SendVoiceAsync(announcement.ChatId, voice: InputFile.FromStream(await _blobServiceManager.GetContentAsStream(announcement.ContentUrl)), caption: announcement.Text, replyMarkup: announcement.ReplyMarkup),
            MessageType.Audio => await _telegramBotClient.SendAudioAsync(announcement.ChatId, audio: InputFile.FromUri(announcement.ContentUrl), caption: announcement.Text),
            _ => null
        };
    }
}

