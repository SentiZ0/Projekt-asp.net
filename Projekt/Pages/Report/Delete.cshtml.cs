using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Report
{
    public class DeleteModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public DeleteModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        [BindProperty]
      public Animals Animals { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animals = await _context.Animals.FirstOrDefaultAsync(m => m.Id == id);

            if (animals == null)
            {
                return NotFound();
            }
            else 
            {
                Animals = animals;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animals = await _context.Animals.FindAsync(id);

            if (animals != null)
            {
                Animals = animals;
                _context.Animals.Remove(Animals);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
