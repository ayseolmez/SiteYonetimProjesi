using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class DuesController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SiteManagementDbContext _context;
        private readonly IDuesService _duesService;

        public DuesController(UserManager<AppUser> userManager, SiteManagementDbContext context, IDuesService duesService)
        {
            _userManager = userManager;
            _context = context;
            _duesService = duesService;
        }
        public IActionResult DuesList()
        {
            var dues= _duesService.TGetList();
            int totalDues = 0;

            foreach (var item in dues)
            {
                if (item.State == State.Ödenmedi)
                {
                    totalDues += item.Amount;
                }
            }
            ViewData["totalDues"] = totalDues;

            ViewBag.user = dues;
            return View();
        }

        public async Task<IActionResult> DuesListOwner()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var dues = _context.Dues.Where(x => x.AppUserID == user.Id).ToList();

            ViewBag.user = dues;


            return View();
        }


        public async Task<IActionResult> PayDues(int id)
        {
            var payableDues = await _context.Dues.Where(x => x.DuesID == id).FirstOrDefaultAsync();


            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var cards = await _context.CreditCards.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();

            if (cards == null)
            {
                return RedirectToAction("AddCreditCard", "CreditCard", new {area =""});
            }

            var card = user.Cards.FirstOrDefault();
            if (card.Remainder > payableDues.Amount)
            {
                payableDues.State = State.Ödendi;
                _duesService.TUpdate(payableDues);


                card.Remainder = card.Remainder - payableDues.Amount;
                _context.CreditCards.Update(card);
                _context.SaveChanges();
            }
            return RedirectToAction("DuesListOwner");

        }
    }
}

