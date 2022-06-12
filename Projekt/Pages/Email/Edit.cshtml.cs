using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    public class EditModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public EditModel(Projekt.Data.ShelterDbContext context)
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

            var letterbox =  await _context.Letterboxes.FirstOrDefaultAsync(m => m.Id == id);
            if (letterbox == null)
            {
                return NotFound();
            }
            Letterbox = letterbox;
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(Letterbox).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LetterboxExists(Letterbox.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool LetterboxExists(int id)
        {
          return (_context.Letterboxes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
