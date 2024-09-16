using System.Threading.Tasks;
using AnnouncementProcessor.ViewModels;
using Telegram.Bot.Types;

namespace AnnouncementProcessor.Managers
{
	public interface IInputFileManager
	{
		Task<InputFile> GetInputFile(AnnouncementMessage announcement);
	}
}

