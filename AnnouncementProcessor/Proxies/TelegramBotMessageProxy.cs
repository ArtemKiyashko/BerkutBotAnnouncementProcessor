using System;
using AnnouncementProcessor.ViewModels;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AnnouncementProcessor.Proxies
{
    public class TelegramBotMessageProxy : ITelegramBotMessageProxy
    {
        private readonly ITelegramBotClient _telegramBotClient;

        public TelegramBotMessageProxy(ITelegramBotClient telegramBotClient)
        {
            _telegramBotClient = telegramBotClient;
        }

        public async Task<Message> SendAnnouncement(AnnouncementMessage announcement) => announcement.MessageType switch
        {
            MessageType.Text => await _telegramBotClient.SendTextMessageAsync(announcement.ChatId, text: announcement.Text),
            MessageType.Photo => await _telegramBotClient.SendPhotoAsync(announcement.ChatId, photo: announcement.ContentUrl.AbsoluteUri, caption: announcement.Text),
            MessageType.Video => await _telegramBotClient.SendVideoAsync(announcement.ChatId, video: announcement.ContentUrl.AbsoluteUri, caption: announcement.Text),
            MessageType.VideoNote => await _telegramBotClient.SendVideoNoteAsync(announcement.ChatId, videoNote: announcement.ContentUrl.AbsoluteUri),
            MessageType.Voice => await _telegramBotClient.SendVoiceAsync(announcement.ChatId, voice: announcement.ContentUrl.AbsoluteUri),
            _ => null
        };
    }
}

