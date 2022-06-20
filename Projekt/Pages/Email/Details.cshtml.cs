using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public DetailsModel(Projekt.Data.ShelterDbContext context)
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
            if (letterbox == null || letterbox.ReceiverId != User.Identity.Name)
            {
                return NotFound();
            }
            else
            {
                Letterbox = letterbox;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostRedirect(int Id)
        {
            return RedirectToPage("./Response", new { id = Id });
        }

        public async Task<IActionResult> OnPostReturn()
        {
            return RedirectToPage("./Index");
        }
    }
}
