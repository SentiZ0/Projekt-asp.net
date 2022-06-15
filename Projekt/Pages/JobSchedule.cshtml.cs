using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages
{
    [Authorize]
    public class JobScheduleModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;

        public JobScheduleModel(ILogger<IndexModel> logger, ShelterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public Job Job { get; set; } = default!;

        [BindProperty]
        public Post Post { get; set; }

        public IList<Job> Jobs { get; set; }

        public async Task OnGetAsync()
        {
            if (_context.Jobs != null)
            {
                Jobs = await _context.Jobs.ToListAsync();
            }
        }
    }
}