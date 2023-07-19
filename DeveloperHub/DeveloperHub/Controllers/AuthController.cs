using DeveloperHub.Data;
using DeveloperHub.Data.DTOs;
using DeveloperHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DeveloperHub.Controllers
{
    public class AuthController : Controller
    {

        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;


        public AuthController(UserManager<ApplicationUser> userManager,
                       SignInManager<ApplicationUser> signInManager, AppDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _context = context;
            _roleManager = roleManager;
        }

        public async Task<IActionResult> Login()
        {
            return View(new Login());
        }


        public async Task<IActionResult> Signup()
        {
            return View(new Register());
        }


       

        [HttpPost]
        public async Task<IActionResult> Signup(Register register)
        {
            if (!ModelState.IsValid)
            {
                return View(register);
            }
            var isRegistered = await _userManager.FindByEmailAsync(register.Email);
            if(isRegistered != null)
            {
                TempData["Error"] = "User is already registered";
                return View(register);
            }

            await CreateRoleIfNotExists(Roles.Admin);
            await CreateRoleIfNotExists(Roles.User);

            var user = new ApplicationUser
            {
                UserName = register.Username,
                Email = register.Email
            };
            var newUser = await _userManager.CreateAsync(user, register.Password);

            if (newUser.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, Roles.User);
                _context.SaveChanges();
                return View( "RegisterCompleted");
            }
            else
            {
                foreach (var error in newUser.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
                return View(register);
            }

            

        }

        private async Task CreateRoleIfNotExists(string roleName)
        {
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                await _roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }

    }
}
