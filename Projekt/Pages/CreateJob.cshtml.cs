using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;
using Projekt.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Pages
{
    [Authorize]
    public class CreateJobModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public CreateJobModel(ILogger<IndexModel> logger, ShelterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }


        public async Task OnPostAsync()
        {
            _context.Jobs.Add(Job);
            _context.SaveChanges();
        }
    }
}
