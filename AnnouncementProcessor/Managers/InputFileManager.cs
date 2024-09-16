using System.Threading.Tasks;
using AnnouncementProcessor.ViewModels;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace AnnouncementProcessor.Managers
{
    public class InputFileManager : IInputFileManager
    {
        private readonly IBlobServiceManager _blobServiceManager;

        public InputFileManager(IBlobServiceManager blobServiceManager)
        {
            _blobServiceManager = blobServiceManager;
        }

        public async Task<InputFile> GetInputFile(AnnouncementMessage announcement)
            => announcement.MessageType switch
            {
                MessageType.Video => announcement.ContentProperties is null ? InputFile.FromUri(announcement.ContentUrl) : InputFile.FromStream(await _blobServiceManager.GetContentAsStream(announcement.ContentUrl)),
                MessageType.Voice => InputFile.FromStream(await _blobServiceManager.GetContentAsStream(announcement.ContentUrl)),
                MessageType.Sticker => InputFile.FromFileId(announcement.Text),
                _ => InputFile.FromUri(announcement.ContentUrl),
            };
    }
}

