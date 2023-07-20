using DeveloperHub.Data;
using DeveloperHub.Data.DTOs;
using DeveloperHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Win32;

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
        [HttpPost]
        public async Task<IActionResult> Login(Login login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }

            var user = await _userManager.FindByEmailAsync(login.Username_or_Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(login.Username_or_Email);
            }
            if (user == null)
            {
                TempData["Error"] = "User with this username or email does not exist";
                return View(login);
            }
            var checkPassword = await _userManager.CheckPasswordAsync(user, login.Password);
            if (!checkPassword)
            {
                TempData["Error"] = "Invalid password";
                return View(login);
            }
            var result = await _signInManager.PasswordSignInAsync(user, login.Password, false, false);
            if (result.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return View(login);
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
            var isRegistered2 = await _userManager.FindByNameAsync(register.Username);
            if(isRegistered != null || isRegistered2!=null)
            {
                TempData["Error"] = "User with this email or username is already registered";
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
