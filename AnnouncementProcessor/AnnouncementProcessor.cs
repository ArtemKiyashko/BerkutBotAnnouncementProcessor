using System;
using System.Threading.Tasks;
using AnnouncementProcessor.Proxies;
using AnnouncementProcessor.ViewModels;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace AnnouncementProcessor
{
    public class AnnouncementProcessor
    {
        private readonly ILogger<AnnouncementProcessor> _logger;
        private readonly ITelegramBotMessageProxy _telegramBotMessageProxy;

        public AnnouncementProcessor(
            ILogger<AnnouncementProcessor> log,
            ITelegramBotMessageProxy telegramBotMessageProxy)
        {
            _logger = log;
            _telegramBotMessageProxy = telegramBotMessageProxy;
        }

        [FunctionName("AnnouncementProcessor")]
        public async Task Run([ServiceBusTrigger("announcements", "announcementprocessor", Connection = "ServiceBusConnection")] AnnouncementMessage announcement)
        {
            await _telegramBotMessageProxy.SendAnnouncement(announcement);
        }
    }
}

