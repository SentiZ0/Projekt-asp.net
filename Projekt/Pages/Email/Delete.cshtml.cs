using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    public class DeleteModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public DeleteModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Letterbox Letterbox { get; set; } = default!;

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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Letterboxes == null)
            {
                return NotFound();
            }
            var letterbox = await _context.Letterboxes.FindAsync(id);

            if (letterbox != null)
            {
                Letterbox = letterbox;
                _context.Letterboxes.Remove(Letterbox);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
