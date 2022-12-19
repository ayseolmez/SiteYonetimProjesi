using Microsoft.AspNetCore.Mvc;
using SiteYonetim.Business.Abstract;
using SiteYonetim.Entities.Concrete;
using System.ComponentModel.DataAnnotations;
using System;
using SiteYonetim.DataAccess.Context;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Message = SiteYonetim.Entities.Concrete.Message;
using System.Linq;
using SiteYonetim.Entities.Enums;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Controllers
{
	public class MessageController : Controller
	{
        private readonly IMessageService _messageService;
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;


        public MessageController(IMessageService messageService, SiteManagementDbContext context, UserManager<AppUser> userManager)
        {
            _messageService = messageService;
            _context = context;
            _userManager = userManager;
        }

        public ActionResult Inbox()
        {

            var messageList = _messageService.TGetlistInbox().OrderByDescending(x=>x.Date);

            ViewBag.messageList = messageList;
            return View();
        }

        public ActionResult InboxDetails(int id)
        {
            var messageDetails = _messageService.TGetByID(id);
            messageDetails.State = MessageState.Read;
            _messageService.TUpdate(messageDetails);
            return View(messageDetails);
        }


        [HttpGet]
        public ActionResult NewMessage()
        {

            return View();
        }


        [HttpPost]
        public async Task <ActionResult> NewMessage(Message message)
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);

            if (ModelState.IsValid)
            {
                message.Date= DateTime.Now;
                message.SenderMail = user.Email;
                message.State = MessageState.Unread;
                _messageService.TAdd(message);
                return RedirectToAction("ManagerHomePage", "Apartment", new {area = "Manager"});

            }
            else
            {
                TempData["mesaj"] = "Eksik alan doldurdunuz";
            }
           
            return View();

        }

    }
}
