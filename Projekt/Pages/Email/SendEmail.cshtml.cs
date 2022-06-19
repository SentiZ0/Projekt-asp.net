using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages
{
    [Authorize]
    public class SendEmailModel : PageModel
    {
        private readonly ShelterDbContext _context;

        private readonly ILogger<IndexModel> _logger;

        public SendEmailModel(ShelterDbContext context, ILogger<IndexModel> logger)
        {
            _context = context;
            _logger = logger;
        }

        [BindProperty]
        public Letterbox Letterbox { get; set; }

        public IActionResult OnPost()
        {
            if(Letterbox.Title == null || Letterbox.Content == null || Letterbox.ReceiverId == null)
            {
                return Page();
            }

            Letterbox.SenderId = User.Identity.Name;
            Letterbox.MailDate = DateTime.Now;

            _context.Letterboxes.Add(Letterbox);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostReturn()
        {
            return RedirectToPage("./Index");
        }

        public void OnGet()
        {
        }
    }
}
