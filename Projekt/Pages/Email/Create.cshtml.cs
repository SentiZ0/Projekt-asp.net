using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
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
        public Letterbox Letterbox { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Letterboxes == null || Letterbox == null)
            {
                return Page();
            }

            _context.Letterboxes.Add(Letterbox);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
