using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Web;
using Projekt.Models;
using Microsoft.AspNetCore.Http;
using Projekt.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateAdoptModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;
        public CreateAdoptModel(IHostEnvironment environment, ILogger<IndexModel> logger, ShelterDbContext context)
        {
            _environment = environment;
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Adoption Adoption { get; set; }

        public string AlertMessage { get; set; }

        [BindProperty]
        public List<IFormFile> FormFiles { get; set; }

        public async Task OnPostAsync(int id)
        {
            var files = new List<AdoptionFileEntity>();

            foreach (var aformFile in FormFiles)
            {
                var fileEntity = new AdoptionFileEntity();

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

            Adoption.FilePaths = files;
            Adoption.AdoptionDate = DateTime.Now;

            _context.Adoptions.Add(Adoption);
            _context.SaveChanges();
        }
        public void OnGet()
        {
        }
    }
}