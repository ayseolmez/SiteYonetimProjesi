using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Controllers
{
	public class CreditCardController : Controller
	{
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public CreditCardController(SiteManagementDbContext context, UserManager<AppUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult AddCreditCard()
		{
			return View();
		}
		[HttpPost]
        public async Task <IActionResult> AddCreditCard(CreditCard model)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            CreditCard card = new CreditCard();
            card.AppUserID = user.Id;
            card.CardNumber = model.CardNumber;
            card.ExpirationDate = model.ExpirationDate;
            card.CVV = model.CVV;
            card.Remainder = 2000;
            _context.Add(card);
            _context.SaveChanges();

            return RedirectToAction("CardList");
        }
        public async Task <IActionResult> CardList()
        {

            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var cards = _context.CreditCards.Where(x => x.AppUserID == user.Id).ToList();

            ViewBag.cards = cards;
            return View();
        }
    }
}
