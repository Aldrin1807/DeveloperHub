using DeveloperHub.Data.DTOs;
using DeveloperHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewComponents;
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



        public async Task<IActionResult> Topics (int id)
        {
            var topics = await _context.Topics.Where(t => t.CategoryID==id)
                .Include(x => x.Category)
                .Include(x => x.ApplicationUser)
                .ToListAsync();



            return View(topics);

        }

        [Authorize(Roles = "Admin")]
        [Authorize]
        public async Task<IActionResult> Create()
        {
            return View();
        }



        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult>  Create(CategoryDTO category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var newCategory = new Category()
            {
                Name = category.Name
            };
            await _context.Categories.AddAsync(newCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }   

    }
}
