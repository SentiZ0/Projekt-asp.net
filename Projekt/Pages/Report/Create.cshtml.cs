using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Report
{
    public class CreateModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public CreateModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Animals Animals { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Animals == null || Animals == null)
            {
                return Page();
            }

            _context.Animals.Add(Animals);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
