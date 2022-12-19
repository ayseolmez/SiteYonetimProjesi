using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using SiteYonetim.UI.Areas.Manager.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class BillsController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly SiteManagementDbContext _context;

        public BillsController(UserManager<AppUser> userManager, IBillsService billsService, SiteManagementDbContext siteManagementDbContext)
        {
            _userManager = userManager;
            _billsService = billsService;
            _context = siteManagementDbContext;
        }

        //public IActionResult CreateBill()
        //{
        //    return View();
        //}

        //[HttpPost]
        //public async Task<IActionResult> CreateBill(AppUser model)
        //{
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);

        //    Bills fatura = new Bills();

        //    fatura.AppUserID = user.Id;

        //    _billsService.TAdd(fatura);

        //    return View();

        //}

        public IActionResult BillsList()
        {
            var bills = _billsService.TGetList();
            int totalBills = 0;

            foreach (var item in bills)
            {
                if (item.State == State.Ödenmedi)
                {
                    totalBills += item.Amount;
                }
            }
            ViewData["total"] = totalBills;


            ViewBag.bills = bills;
            return View();
        }


        public async Task <IActionResult> BillsListOwner()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var bills = _context.Bills.Where(x => x.AppUserID == user.Id).ToList();

            ViewBag.bills = bills;

            return View();
        }

        public async Task<IActionResult> PayBills(int id)
        {
            var payableBills = await _context.Bills.Where(x => x.BillID == id).FirstOrDefaultAsync();


            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            var cards = await _context.CreditCards.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();

            if (cards == null)
            {
                return RedirectToAction("AddCreditCard", "CreditCard", new { area = "" });
            }

            var card = user.Cards.FirstOrDefault();
            if (card.Remainder > payableBills.Amount)
            {
                payableBills.State = State.Ödendi;
                _billsService.TUpdate(payableBills);


                card.Remainder = card.Remainder - payableBills.Amount;
                _context.CreditCards.Update(card);
                _context.SaveChanges();
            }
            return RedirectToAction("BillsListOwner");

        }
    }
}
