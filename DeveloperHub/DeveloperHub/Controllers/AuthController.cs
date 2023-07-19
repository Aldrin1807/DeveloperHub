using Microsoft.AspNetCore.Mvc;

namespace DeveloperHub.Controllers
{
    public class AuthController : Controller
    {

      public async Task<IActionResult> Login()
        {
            return View();
        }


        public async Task<IActionResult> Signup()
        {
            return View();
        }
    }
}
