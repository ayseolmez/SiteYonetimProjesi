using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Entities.Concrete;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Controllers
{
    public class RoleController : Controller
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleController(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(AppRole appRole)
        {
            if (ModelState.IsValid)
            {
                var result = await _roleManager.CreateAsync(appRole);

                if (result.Succeeded)
                {
                    return RedirectToAction("Login", "Login");
                }
            }

            return View();
        }
    }
}
