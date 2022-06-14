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

namespace Projekt.Pages.Report
{
    public class EditModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;
        private readonly IHostEnvironment _environment;
        public EditModel(Projekt.Data.ShelterDbContext context, IHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        [BindProperty]
        public Animals Animals { get; set; } = default!;

        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animals =  await _context.Animals.FirstOrDefaultAsync(m => m.Id == id);
            if (animals == null)
            {
                return NotFound();
            }
            Animals = animals;
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

            _context.Attach(Animals).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AnimalsExists(Animals.Id))
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

        public async Task<IActionResult> OnPostAccept(int id) 
        {
            var animals = await _context.Animals.FirstOrDefaultAsync(m => m.Id == id);
            animals.Accepted = true;

            Animals = animals;

            _context.Attach(Animals).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostReject(int id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animals = await _context.Animals.FindAsync(id);

            string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{animals.FilePath}";

            if (System.IO.File.Exists(targetFileName))
            {
                System.IO.File.Delete(targetFileName);
            }

            if (animals != null)
            {
                Animals = animals;
                _context.Animals.Remove(Animals);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostReturn()
        {
            return RedirectToPage("./Index");
        }
        private bool AnimalsExists(int id)
        {
          return (_context.Animals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
