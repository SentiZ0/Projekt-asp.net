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

        public IList<Animals> Animals { get; set; }

        public IList<Post> Posts { get; set; }

        public List<FileEntity> FileEntities { get; set; } = default!;

        [BindProperty]
        public string Comment { get; set; }

        public async Task<IActionResult> OnPostReject(int id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }
            var animal = await _context.Animals.Include(m => m.Posts).Include(m => m.FilePaths).FirstOrDefaultAsync(m => m.Id == id);

            foreach (var item in animal.FilePaths)
            {
                string targetFileName = $"{_environment.ContentRootPath}/wwwroot/{item.FilePath}";

                if (System.IO.File.Exists(targetFileName))
                {
                    System.IO.File.Delete(targetFileName);
                }
            }


            if (animal != null)
            {
                Animal = animal;
                _context.Animals.Remove(Animal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }

        public async Task<IActionResult> OnPostAsync(int id)
         {
            var Animal = await _context.Animals.Include(m => m.Posts).Include(m => m.FilePaths).FirstOrDefaultAsync(m => m.Id == id);
            var post = new Post();

            post.Content = Comment;
            post.PostDate = DateTime.Now;
            post.UserName = User.Identity.Name;

            Animal.Posts.Add(post);

            _context.Attach(Animal).State = EntityState.Modified;
            _context.SaveChanges();

            return RedirectToPage("./Index");
        }

        public async Task OnGetAsync()
        {
            Animals = await _context.Animals.Where(a => a.Accepted).OrderByDescending(a => a.ReportDate).ToListAsync();

            Posts = await _context.Posts.ToListAsync();

            FileEntities = await _context.FileEntities.ToListAsync();
        }
    }
}