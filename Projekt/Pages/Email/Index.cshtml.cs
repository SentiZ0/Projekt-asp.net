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
    public class IndexModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public IndexModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        public IList<Letterbox> Letterbox { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Letterboxes != null)
            {
                Letterbox = await _context.Letterboxes.Where(a=>a.ReceiverId == User.Identity.Name).OrderByDescending(a => a.MailDate).ToListAsync();
            }
        }
    }
}
