using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Azure.Storage.Blobs;

namespace AnnouncementProcessor.Managers
{
	public class BlobServiceManager : IBlobServiceManager
	{
        private const string PUBLIC_CONTAINER = "public";

        private readonly BlobServiceClient _blobServiceClient;

        public BlobServiceManager(BlobServiceClient blobServiceClient)
		{
            _blobServiceClient = blobServiceClient;
        }

        public async Task<Stream> GetContentAsStream(Uri uri)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(PUBLIC_CONTAINER);
            var blobClient = containerClient.GetBlobClient(string.Concat(uri.Segments.AsSpan(2).ToArray()));
            var blobStreamResponse = await blobClient.DownloadStreamingAsync();

            return blobStreamResponse.Value.Content;
        }
    }
}

