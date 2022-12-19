using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System;
using Microsoft.AspNetCore.Identity;
using SiteYonetim.Entities.Concrete;
using System.Threading.Tasks;
using System.Linq;
using SiteYonetim.Entities.Enums;

namespace SiteYonetim.UI.Controllers
{
	public class WriterController : Controller
	{
        private readonly IMessageService _messageService;
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        public WriterController(IMessageService messageService, SiteManagementDbContext context, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
        }

        public async Task<ActionResult> InboxOwner()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var messageList = _context.Messages.Where(x => x.ReceiverMail == user.Email).ToList().OrderByDescending(x => x.Date);
            ViewBag.messagelist = messageList;
            return View();
        }

        public async Task <ActionResult> InboxDetailsOwner(int id)
        {
            var messageDetails = _messageService.TGetByID(id);
            messageDetails.State = MessageState.Read;
            _messageService.TUpdate(messageDetails);
            return View(messageDetails);
        }


        [HttpGet]
        public ActionResult NewMessageOwner()
        {
            return View();
        }


        [HttpPost]
        public async Task <ActionResult> NewMessageOwner(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            if (ModelState.IsValid)
            {

                message.Date = DateTime.Now;
                message.SenderMail = user.Email;
                message.State = MessageState.Unread;
                _messageService.TAdd(message);
                return RedirectToAction("OwnerHomePage", "Apartment", new { area = "Manager" });

            }
            else
            {
                TempData["mesaj"] = "Eksik alan doldurdunuz";
            }
           

            return View();


        }

    }
}

