using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Controllers
{
    public class CategoryController : Controller
    {
        private readonly AppDbContext _context;

        public CategoryController(AppDbContext context)
        {
                    _context = context;
        }   

        public async Task<IActionResult> Index()
        {
            var categories =await _context.Categories
                .ToListAsync();
            foreach (var category in categories)
            {
                category.topicCount = _context.Topics.Count(t => t.CategoryID == category.Id);
            }

            return View(categories);
        }
    }
}
