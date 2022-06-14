using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Projekt.Models;
using Microsoft.AspNetCore.Http;
using Projekt.Data;

namespace Projekt.Pages
{
    public class CreateReportModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;
        public CreateReportModel(IHostEnvironment environment, ILogger<IndexModel> logger, ShelterDbContext context)
        {
            _environment = environment;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Animals Animals { get; set; }

        public string AlertMessage { get; set; }

        [BindProperty]
        public IFormFile UploadedFile { get; set; }

        public async Task OnPostAsync()
        {
            var checkType = UploadedFile.ContentType;
            var fileSize = UploadedFile.Length;
            var fileCounter = 1;
            bool stopLoop = true;

            if (UploadedFile == null || UploadedFile.Length == 0)
            {
                AlertMessage = "Nie wybrano/odnaleziono pliku.";
                return;
            }
            else if (!(checkType.Contains("image")))
            {
                AlertMessage = "Wybrano niepoprawny format pliku. Obs³ugiwane formaty to gif/jpeg/png/webp.";
                return;
            }
            else if (fileSize > 1048576)
            {
                AlertMessage = "Plik nie mo¿e przekraczaæ 10mb";
                return;
            }

            _logger.LogInformation($"Uploading {UploadedFile.FileName}.");

            string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{UploadedFile.FileName}";

            while(stopLoop != false)
            {
                if (System.IO.File.Exists(targetFileName))
                {
                    fileCounter++;
                    targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{UploadedFile.FileName}";
                }
                else
                {
                    stopLoop = false;
                }
            }

            using (var stream = new FileStream(targetFileName, FileMode.Create))
            {
                await UploadedFile.CopyToAsync(stream);
            }

            Animals.FilePath = $"{fileCounter}{UploadedFile.FileName}";
            Animals.ReportDate = DateTime.Now;
            Animals.Accepted = false;

            _context.Animals.Add(Animals);
            _context.SaveChanges();
        }
        public void OnGet()
        {
        }
    }
}
