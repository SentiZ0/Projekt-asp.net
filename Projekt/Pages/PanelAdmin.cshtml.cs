using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Projekt.Pages
{
    [Authorize(Policy = "AdminOnly")]
    public class PanelAdminModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
