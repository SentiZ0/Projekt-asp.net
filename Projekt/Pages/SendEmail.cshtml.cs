using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;

namespace Projekt.Pages
{
    public class SendEmailModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public SendEmailModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public Mail Mail { get; set; }

        public IActionResult OnPost()
        {
            Mail.SenderId = User.Identity.Name;
            Mail.MailDate = DateTime.Now;
            return Page();
        }
        public void OnGet()
        {
        }
    }
}
