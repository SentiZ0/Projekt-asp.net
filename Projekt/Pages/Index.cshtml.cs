using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;

namespace Projekt.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ShelterDbContext _context;
        private readonly ILogger<IndexModel> _logger;
        private readonly IHostEnvironment _environment;

        public IndexModel(ILogger<IndexModel> logger, ShelterDbContext context, IHostEnvironment environment)
        {
            _logger = logger;
            _context = context;
            _environment = environment;
        }

        public Animals Animal { get; set; } = default!;

        [BindProperty]
        public Post Post { get; set; }

        public IList<Animals> Animals { get; set; }

        public IList<Post> Posts { get; set; }

        public IFormFile UploadedFile { get; set; }

        public async Task<IActionResult> OnPostReject(int id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animal = await _context.Animals.FindAsync(id);

            string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{animal.FilePath}";

            if (System.IO.File.Exists(targetFileName))
            {
                System.IO.File.Delete(targetFileName);
            }

            if (animal != null)
            {
                Animal = animal;
                _context.Animals.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public IActionResult OnPost(int id)
        {
            Post.AnimalsId = id;
            Post.PostDate = DateTime.Now;
            Post.UserName = User.Identity.Name;

            _context.Posts.Add(Post);
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        public async Task OnGetAsync()
        {
            Animals = await _context.Animals.Where(a=>a.Accepted).OrderByDescending(a=>a.ReportDate).ToListAsync();

            Posts = await _context.Posts.ToListAsync();
        }
    }
}