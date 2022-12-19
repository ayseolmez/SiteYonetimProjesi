using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.EntityFrameworkCore;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System;
using System.Linq;

namespace SiteYonetim.UI.Controllers
{
    public class MonthlyPaymentController : Controller
    {
        private readonly SiteManagementDbContext _context;
        private readonly IBillsService _billsService;
        private readonly IDuesService _duesService;
        public MonthlyPaymentController(SiteManagementDbContext context, IBillsService billsService, IDuesService duesService)
        {
            _context = context;
            _billsService = billsService;
            _duesService = duesService;
        }

        public void MonthlyDuty()
        {
            var users = _context.Users.Include("Apartment").Where(x => x.UserName.Contains("admin") == false).ToList();

            foreach (var user in users)
            {
                Bills bill = new Bills();
                bill.AppUserID = user.Id;
                bill.State = State.Ödenmedi;
                bill.Date = DateTime.Now;
                bill.Amount = 500;
                _billsService.TAdd(bill);

                Dues dues = new Dues();
                dues.AppUserID = user.Id;
                dues.Date = DateTime.Now;
                dues.Amount = 500;
                dues.State = State.Ödenmedi;
                _duesService.TAdd(dues);
            }
        }

        [HttpGet]
        public IActionResult Monthly()
        {
            RecurringJob.AddOrUpdate(() => MonthlyDuty(), "0 0 1 * *");
            return Ok($"Bill and dues assigned every month.");
        }

    }
}
