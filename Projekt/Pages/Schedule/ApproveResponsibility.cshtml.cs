using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Schedule
{
    [Authorize(Policy="AdminOnly")]
    public class ApproveResponsibilityModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;
        public ApproveResponsibilityModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

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

    
