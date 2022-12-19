using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Entities.Concrete;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Controllers
{
    public class OwnerController : Controller
    {
        private readonly UserManager<AppUser> _userManager;

        public OwnerController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<IActionResult> MyProfile()
        {
            var personel = await _userManager.FindByEmailAsync(User.Identity.Name);
            return View(personel);
        }
    }
}
