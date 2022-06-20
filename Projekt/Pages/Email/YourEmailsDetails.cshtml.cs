using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    [Authorize]
    public class YourEmailsDetailsModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public YourEmailsDetailsModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        public Letterbox Letterbox { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Letterboxes == null)
            {
                return NotFound();
            }

            var letterbox = await _context.Letterboxes.FirstOrDefaultAsync(m => m.Id == id);
            if (letterbox == null || letterbox.SenderId != User.Identity.Name)
            {
                return NotFound();
            }
            else
            {
                Letterbox = letterbox;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostReturn()
        {
            return RedirectToPage("./Index");
        }
    }

}
