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

namespace Projekt.Pages.Report
{
    [Authorize(Policy = "AdminOnly")]
    public class IndexModel : PageModel
    {
        private readonly Projekt.Data.ShelterDbContext _context;

        public IndexModel(Projekt.Data.ShelterDbContext context)
        {
            _context = context;
        }

        public IList<Animals> Animals { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Animals != null)
            {
                Animals = await _context.Animals.Where(a=>a.Accepted == false).ToListAsync();
            }
        }
    }
}
