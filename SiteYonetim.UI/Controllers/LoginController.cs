using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.UI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;

        private readonly UserManager<AppUser> _userManager;


        public LoginController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UserLoginModel appUser)
        {


            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(appUser.UserName, appUser.Password, false, true);

                if (result.Succeeded)
                {
                 

                    return RedirectToAction("ManagerHomePage", "Apartment", new { area = "Manager" });


                }

                ModelState.AddModelError("", "Kullanıcı adı veya şifreniz hatalı");
            }


            return View();
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();

            return RedirectToAction("Login", "Login");
        }
    }
}
