using System;
using System.Threading.Tasks;
using AnnouncementProcessor.ViewModels;
using Telegram.Bot.Types;

namespace AnnouncementProcessor.Proxies
{
    public interface ITelegramBotMessageProxy
    {
        Task<Message> SendAnnouncement(AnnouncementMessage announcementRequest);
    }
}

