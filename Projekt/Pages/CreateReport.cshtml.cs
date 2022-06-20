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

        public async Task<IActionResult> OnPostAsync(int id)
        {
            if (Animals.Title == null || Animals.Description == null)
            {
                return Page();
            }

            var files = new List<FileEntity>();

            if (FormFiles == null)
            {
                AlertMessage = "Nie wybrano/odnaleziono pliku.";
                return Page();
            }

            foreach (var aformFile in FormFiles)
            {
                var fileEntity = new FileEntity();

                var formFile = aformFile;

                var fileSize = formFile.Length;
                var checkType = formFile.ContentType;
                var fileCounter = 1;


                if (!(checkType.Contains("image")))
                {
                    AlertMessage = "Wybrano niepoprawny format pliku. Obs³ugiwane formaty to gif/jpeg/png/webp.";
                    return Page();
                }
                else if (fileSize > 1048576)
                {
                    AlertMessage = "Plik nie mo¿e przekraczaæ 10mb";
                    return Page();
                }

                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";

                while (System.IO.File.Exists(targetFileName))
                {
                    fileCounter++;
                    targetFileName = $"{_environment.ContentRootPath}/wwwroot/{fileCounter}{formFile.FileName}";
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

            return Page();
        }
        public void OnGet()
        {
        }
    }
}
