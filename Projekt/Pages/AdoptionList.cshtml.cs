using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages
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

        public async Task OnGetAsync()
        {
            Adoptions = await _context.Adoptions.OrderByDescending(a => a.AdoptionDate).ToListAsync();

            AdoptionFileEntities = await _context.AdoptionFiles.ToListAsync();
        }
    }
}
