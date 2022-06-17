using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages.Email
{
    public class YourEmailsModel : PageModel
    {
        private readonly ShelterDbContext _context;

        private readonly ILogger<IndexModel> _logger;


        public IList<Letterbox> Letterbox { get; set; } = default!;

        public YourEmailsModel(ShelterDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }
        public async Task OnGetAsync()
        {
            Letterbox = await _context.Letterboxes.Where(a => a.SenderId == User.Identity.Name).OrderByDescending(a => a.MailDate).ToListAsync();
        }
    }
}
