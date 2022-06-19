using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Projekt.Models;
using Projekt.Data;
using Microsoft.AspNetCore.Authorization;

namespace Projekt.Pages.Schedule
{
    [Authorize(Policy = "AdminOnly")]
    public class CreateJobModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        public CreateJobModel(ILogger<IndexModel> logger, ShelterDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        [BindProperty]
        public Job Job { get; set; }

        public string AlertMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {

            if(Job.JobStartDate<DateTime.Now)
            {
                AlertMessage = "Data rozpoczêcia nie mo¿e byæ wczeœniejsza ni¿ teraŸniejsza.";
                return Page();
            }
            if(Job.JobEndDate<Job.JobStartDate)
            {
                AlertMessage = "Data zakoñczenia nie mo¿e byæ wczeœniejsza od daty rozpoczêcia.";
                return Page();
            }
            if(Job.Responsibility==null)
            {
                AlertMessage = "Pole obowi¹zek nie mo¿e byæ puste.";
                return Page();
            }

            _context.Jobs.Add(Job);
            _context.SaveChanges();

            return RedirectToPage("./CreateJob");
        }
    }
}
