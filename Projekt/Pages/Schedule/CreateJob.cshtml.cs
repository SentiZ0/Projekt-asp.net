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
                AlertMessage = "Data rozpocz�cia nie mo�e by� wcze�niejsza ni� tera�niejsza.";
                return Page();
            }
            if(Job.JobEndDate<Job.JobStartDate)
            {
                AlertMessage = "Data zako�czenia nie mo�e by� wcze�niejsza od daty rozpocz�cia.";
                return Page();
            }
            if(Job.Responsibility==null)
            {
                AlertMessage = "Pole obowi�zek nie mo�e by� puste.";
                return Page();
            }

            _context.Jobs.Add(Job);
            _context.SaveChanges();

            return RedirectToPage("./CreateJob");
        }
    }
}
