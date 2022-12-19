using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System.Threading.Tasks;
using Hangfire;
using MailKit.Net.Smtp;
using MimeKit;
using System.Linq;

namespace SiteYonetim.UI.Controllers
{
	public class EmailController : Controller
	{
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly SiteManagementDbContext _context;

        public EmailController(UserManager<AppUser> userManager, IBillsService billsService, SiteManagementDbContext context)
        {
            _userManager = userManager;
            _billsService = billsService;
            _context = context;
        }

        [HttpGet]
        public async Task BillNotPaySendMail()

        {
            var bills = _context.Bills.Where(x => x.State == State.Ödenmedi).ToList();

            foreach (var item in bills)
            {
                SendMail(item);
            }

        }

        public void SendMail(Bills bill)
        {
            MimeMessage mimeMessage = new MimeMessage();

            MailboxAddress mailboxAddressFrom = new MailboxAddress("Admin", "paparasiteyonetimi@gmail.com");
            mimeMessage.From.Add(mailboxAddressFrom); 

            MailboxAddress mailboxAddressTo = new MailboxAddress("User", bill.AppUsers.Email);
            mimeMessage.To.Add(mailboxAddressTo); 

            var bodyBuilder = new BodyBuilder();
            bodyBuilder.TextBody = "Ödenmemiş" + bill.Amount + "TL tutarunda faturanız vardır.";
            mimeMessage.Body = bodyBuilder.ToMessageBody(); 

            mimeMessage.Subject = bill.BillID + " Numaralı Faturanız Ödenmemiştir";

            SmtpClient smtp = new SmtpClient();
            smtp.Connect("smtp.gmail.com", 587, false);
            smtp.Authenticate("paparasiteyonetimi@gmail.com", "bentwpÖx");
            smtp.Send(mimeMessage);
            smtp.Disconnect(true);

        }

        [HttpGet]
        [Route("BillNotPaySendMail")]
        public IActionResult RetrieveData()
        {
            RecurringJob.AddOrUpdate(() => BillNotPaySendMail(), "0 0 1 * * ?");
            return Ok($"Mail send every day.");
        }
    }
}

