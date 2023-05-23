using System;
using System.IO;
using System.Threading.Tasks;

namespace AnnouncementProcessor.Managers
{
	public interface IBlobServiceManager
	{
        Task<Stream> GetContentAsStream(Uri uri);
	}
}

