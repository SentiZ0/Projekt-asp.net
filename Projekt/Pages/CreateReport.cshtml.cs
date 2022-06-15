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
        public List<IFormFile> FormFiles { get; set; }

        public async Task OnPostAsync(int id)
        {
            var files = new List<FileEntity>();

            foreach (var aformFile in FormFiles)
            {
                var fileEntity = new FileEntity();

                var formFile = aformFile;

                var fileSize = formFile.Length;
                var checkType = formFile.ContentType;
                var fileCounter = 1;
                bool stopLoop = true;

                if (formFile == null || formFile.Length == 0)
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

                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";

                while (stopLoop != false)
                {
                    if (System.IO.File.Exists(targetFileName))
                    {
                        fileCounter++;
                        targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";
                    }
                    else
                    {
                        stopLoop = false;
                    }
                }

                using (var stream = new FileStream(targetFileName, FileMode.Create))
                {
                    await formFile.CopyToAsync(stream);
                }

                fileEntity.FilePath = $"{fileCounter}{formFile.FileName}";
                files.Add(fileEntity);
            }

            Animals.ReportDate = DateTime.Now;
            Animals.Accepted = false;
            Animals.FilePaths = files;

            _context.Animals.Add(Animals);
            _context.SaveChanges();
        }
        public void OnGet()
        {
        }
    }
}
