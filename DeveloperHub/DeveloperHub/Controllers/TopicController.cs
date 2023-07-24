using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DeveloperHub.Controllers
{
    public class TopicController : Controller
    {
        private readonly AppDbContext _context;

        public TopicController(AppDbContext context)
        {
                    _context = context;
        }

        //GET /topic/details/1
        public async Task<IActionResult> Details(int id)
        {
            var Topic =await _context.Topics
                .Include(c=>c.Category)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (Topic == null)
                return NotFound();

            var user =await _context.Users.FirstOrDefaultAsync(x => x.Id == Topic.UserID);

            if (user == null)
                return NotFound();

            Topic.ApplicationUser = user;

            return View(Topic);
        }
    }
}
