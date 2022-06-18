using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages
{
    [Authorize]
    public class AddResponsibilityModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public AddResponsibilityModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Job = await _context.Jobs.FindAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            var job = await _context.Jobs.FirstOrDefaultAsync(m => m.Id == id);

            if (job == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }
                Job = await _context.Jobs.FindAsync(id);

                if (Job != null)
                {
                        Job.WorkerMail = User.Identity.Name;
                        _context.Jobs.Update(Job);
                        await _context.SaveChangesAsync();
                        return RedirectToPage("./JobSchedule");
                }
            return RedirectToPage("./JobSchedule");
        }
    }
}