using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using UploadFilesToQuizBowl.Services;
using Microsoft.AspNetCore.Hosting;

namespace UploadFilesToQuizBowl.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private IBlobConnection _blobConnection;
        IWebHostEnvironment _environment;

        public bool isValid = true;
        public bool startedUpload = false;
        public bool finishedUpload = false;
        public bool processing = false;
        public IndexModel(ILogger<IndexModel> logger, IBlobConnection blobConnection, Microsoft.AspNetCore.Hosting.IWebHostEnvironment environment)
        {
            _logger = logger;
            _blobConnection = blobConnection;
            _environment = environment;
        }

        public async void OnPostDownloadFile(IFormFile uploadedFile)
        {




            processing = true;
            
                string[]? entries = null;
                string? name = "";

                if (uploadedFile != null && uploadedFile.FileName != null)
                {
                    using (
                 var ms = new MemoryStream())
                    {
                        uploadedFile?.CopyToAsync(ms);
                        ms.Seek(0, SeekOrigin.Begin);


                        if (ms != null)
                        {

                            using (StreamReader sr = new StreamReader(ms))
                            {
                                entries = sr.ReadToEnd().Split(Environment.NewLine);
                            
                            name = uploadedFile?.FileName;



                            bool validFile = true;
                            if (entries != null)
                            {
                                foreach (string entry in entries)
                                {
                                    string[] splitQuestion = entry.Split('.');
                                    int pointValue = 0;
                                    if (splitQuestion.Length == 4)
                                    {
                                        if (splitQuestion[0].Length > 0 && splitQuestion[1].Length > 0 && splitQuestion[2].Length > 0 && splitQuestion[3].Length > 0)
                                        {
                                            if (int.TryParse(splitQuestion[2], out pointValue))
                                            {
                                                continue;
                                            }
                                            else
                                            {
                                                validFile = false;
                                                break;
                                            }
                                        }
                                        else
                                        {
                                            validFile = false;
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        validFile = false;
                                        break;
                                    }
                                }
                            }


                            if (validFile)
                            {
                                isValid = true;
                                if (ms != null && name != null)
                                {


                                    await _blobConnection.UploadBlobToContainer(ms, name);
                                    processing = false;
                                    
                                }

                            }
                            else
                                isValid = false;
                        }
                    }
                }
            }
        
        }
        public void OnGet()
        {

        }
    }
}