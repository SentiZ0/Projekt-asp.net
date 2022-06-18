using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    [Authorize]
    public class ResponseModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public ResponseModel(ShelterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Letterbox Letterbox { get; set; } = default!;

        public async Task<IActionResult> OnPost(int id)
        {
            var letterbox = await _context.Letterboxes.FirstOrDefaultAsync(m => m.Id == id);

            Letterbox.ReceiverId = letterbox.SenderId;
            Letterbox.SenderId = User.Identity.Name;
            Letterbox.MailDate = DateTime.Now;

            _context.Letterboxes.Add(Letterbox);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostReturn(int Id)
        {
            return RedirectToPage("./Details", new { id = Id });
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Letterboxes == null)
            {
                return NotFound();
            }

            var letterbox = await _context.Letterboxes.FirstOrDefaultAsync(m => m.Id == id);
            if (letterbox == null)
            {
                return NotFound();
            }
            else
            {
                Letterbox = letterbox;
            }
            return Page();
        }        
    }
}
