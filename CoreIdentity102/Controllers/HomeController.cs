using CoreIdentity102.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using CoreIdentity102.ViewModel;
using Microsoft.AspNetCore.Identity;

namespace CoreIdentity102.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<AppUser> _userManager;

        public HomeController(ILogger<HomeController> logger, UserManager<AppUser> userManager)

        {
            _logger = logger;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserViewModel model)
        {
            
            if (ModelState.IsValid)
            {
                var usermail = await _userManager.FindByEmailAsync(model.EMail);
                var pass = await _userManager.CheckPasswordAsync(usermail, model.Password);
                if (pass!=false)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty,"şifre yada mail hatalı"); 
                    
                }

            }

            return View(model);
        }

        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = new AppUser();
                user.Email = model.Email;
                user.UserName = model.UserName;
                user.PhoneNumber = model.PhoneNumber;
                IdentityResult result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (IdentityError item in result.Errors)
                    {
                        ModelState.AddModelError(item.Code,item.Description);
                    }
                }
                
            }

            return View(model);
        }


        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}