using DeveloperHub.Data.DTOs;
using DeveloperHub.Data.Services;
using DeveloperHub.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace DeveloperHub.Controllers
{
    public class HomeController : Controller
    {
      
        private readonly AppDbContext _context;
        private readonly TopicService _service;

        public HomeController(AppDbContext context,TopicService service)
        {
            _context = context;
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }


        [Authorize]
        public async Task<IActionResult> Create()
        {
            var categorys =await _context.Categories.ToListAsync();

            ViewBag.Categories = new SelectList(categorys, "Id", "Name");
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Create(TopicDTO topic)
        {
            if (!ModelState.IsValid)
            {
                var categorys = await _context.Categories.ToListAsync();
                ViewBag.Categories = new SelectList(categorys, "Id", "Name");
                return View(topic);
            }
          await _service.Create(topic);
          return RedirectToAction(nameof(Index));
        }
    }
}