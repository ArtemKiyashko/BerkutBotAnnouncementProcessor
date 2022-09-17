using System;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.ReplyMarkups;

namespace AnnouncementProcessor.ViewModels
{
    public class AnnouncementMessage
    {
        public MessageType MessageType { get; set; }
        public string Text { get; set; }
        public Uri ContentUrl { get; set; }
        public long ChatId { get; set; }
        public InlineKeyboardMarkup ReplyMarkup { get; set; }
    }
}

