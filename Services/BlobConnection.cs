using Azure.Storage.Blobs;

namespace UploadFilesToQuizBowl.Services
{
    public class BlobConnection :  IBlobConnection 
    {
        public IConfiguration _configuration;



        public BlobServiceClient _blobServiceClient { get; set; }
        public BlobContainerClient _blobContainerClient { get; set; }

        public BlobConnection(IConfiguration configuration)
        {
            _configuration = configuration;
           
            SetBlobContainerClient();
        }

        public void SetBlobServiceClient()
        {

            _blobServiceClient = new BlobServiceClient(_configuration["QuizBowlSAS"]);
           
        }
        public  void SetBlobContainerClient()
        {
           // if(_configuration["QuizBowlSAS"] != null)

            _blobContainerClient = new BlobContainerClient(new Uri([redacted]));


        }
  
        public async Task  UploadBlobToContainer(Stream fileToUpload, string Name)
        {

            fileToUpload.Position = 0;

            BlobClient blobClient = _blobContainerClient.GetBlobClient(Name);
            await blobClient.UploadAsync(fileToUpload, true);

            // await _blobContainerClient.UploadBlobAsync(Path.GetFileName(fileToUpload.Name), fileToUpload, overwrite = True);

            return;

        }



    }
}
