using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Announcement
{
    public class AdoptionListModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;

        public AdoptionListModel(ILogger<IndexModel> logger, ShelterDbContext context, IHostEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        public Adoption Adoption { get; set; }
        public List<Adoption> Adoptions { get; set; }

        public List<AdoptionFileEntity> AdoptionFileEntities { get; set; }


        public async Task<IActionResult> OnPostReject(int id)
        {
            if (id == null || _context.Adoptions == null)
            {
                return NotFound();
            }
            var adoption = await _context.Adoptions.Include(m => m.FilePaths).FirstOrDefaultAsync(m => m.Id == id);

            foreach (var item in adoption.FilePaths)
            {
                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{item.FilePath}";

                if (System.IO.File.Exists(targetFileName))
                {
                    System.IO.File.Delete(targetFileName);
                }
            }


            if (adoption != null)
            {
                Adoption = adoption;
                _context.Adoptions.Remove(Adoption);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./AdoptionList");
        }
        public async Task OnGetAsync()
        {
            Adoptions = await _context.Adoptions.OrderByDescending(a => a.AdoptionDate).ToListAsync();

            AdoptionFileEntities = await _context.AdoptionFiles.ToListAsync();
        }
    }
}
