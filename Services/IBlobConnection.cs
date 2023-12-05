using Azure.Storage.Blobs;
using System.Runtime.CompilerServices;

namespace UploadFilesToQuizBowl.Services
{
    public interface IBlobConnection
    {
        public BlobServiceClient _blobServiceClient { get; set; }
        public BlobContainerClient _blobContainerClient { get; set; }
        public void SetBlobServiceClient();
        public void SetBlobContainerClient();

        public  Task UploadBlobToContainer(Stream file, string Name);
    }
}
