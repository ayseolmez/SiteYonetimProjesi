using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using Org.BouncyCastle.Math.EC.Rfc7748;
using SiteYonetim.Business.Abstract;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using SiteYonetim.UI.Areas.Manager.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI.Areas.Manager.Controllers
{
    [Area("Manager")]
    public class ApartmentController : Controller
    {
        private readonly IApartmentService _apartmentService;
        private readonly SiteManagementDbContext _context;
        private readonly UserManager<AppUser> _userManager;
        private readonly IBillsService _billsService;
        private readonly IDuesService _duesService;

        public ApartmentController(SiteManagementDbContext context, IApartmentService apartmentService, UserManager<AppUser> userManager, IBillsService billsService, IDuesService duesService)
        {
            _context = context;
            _apartmentService = apartmentService;
            _userManager = userManager;
            _billsService = billsService;
            _duesService = duesService;
        }


        public async Task <IActionResult> OwnerHomePage()
        {
            var personel = await _userManager.FindByNameAsync(User.Identity.Name);
            return View(personel);
        }

        public async Task<IActionResult> ManagerHomePage()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var rol = await _userManager.GetRolesAsync(user);

            if (rol.Contains("Owner"))
            {
                return RedirectToAction("OwnerHomePage", "Apartment", new { area = "Manager" });
            }
            return View();
        }


        public async Task <IActionResult> OwnerList()
        {
            var userinc = await _context.Users.Include("Apartment").Where(x => x.UserName.Contains("admin") == false).ToListAsync();
            ViewBag.user = userinc;
            
            return View();
        }
        public async Task <IActionResult> ManagerList()
        {
            var admins = await _context.Users.Include("Apartment").Where(x => x.UserName.Contains("admin")).ToListAsync();
            ViewBag.admin = admins;
            return View();
        }
        public IActionResult CreateOwner()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOwner(OwnerCreateModel model)
        {
            AppUser appUser = new AppUser();

            appUser.Name = model.Name;  
            appUser.Surname=model.Surname;
            appUser.IdentityNumber = model.IdentityNumber;
            appUser.LicensePlate = model.LicensePlate;
            appUser.UserName = model.UserName;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Email = model.Email;
            appUser.StatusUser = StatusUser.Aktif;
            

            //burada seçilen daireyi buluyoruz, eğer daire doluysa daireyi seçemiyoruz.
            var apartman1 = _context.Apartments.Where(x => x.Block == model.Block && x.DoorNumber == model.DoorNumber).FirstOrDefault();

            //dbde olmayan bir daire seçerse hata veriyoruz.
            if (apartman1 == null)
            {
                TempData["mesaj"] = "Sitemizde böyle bir daire bulunmamaktadır.";
                return View();
            }
            if (apartman1.Status == Status.Dolu)
            {
                TempData["mesaj"] = "Seçtiğiniz daire dolu bu daireyi seçemezsiniz.";
                return View();
            }

            //Seçilen dairenin tüm özelliklerini liste olarak alıp kullanıcıya atıyoruz.

            var apartman = _context.Apartments.Where(x => x.Block == model.Block && x.DoorNumber == model.DoorNumber).ToList();

            appUser.Apartment = apartman;


            apartman1.Status = Status.Dolu;

            
                var result = await _userManager.CreateAsync(appUser, model.UserName +"123.");


            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(appUser, "Owner");

                Bills bills = new Bills();
                bills.AppUserID = appUser.Id;
                bills.Date = DateTime.Now;
                bills.Amount = _billsService.CalculateBill(apartman1.RoomCount);
                bills.State = State.Ödenmedi;
                _billsService.TAdd(bills);

                Dues dues = new Dues();
                dues.AppUserID = appUser.Id;
                dues.Date = DateTime.Now;
                dues.Amount = 500;
                dues.State = State.Ödenmedi;
                _duesService.TAdd(dues);

                return RedirectToAction("OwnerList");
            }
            else
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View();

        }

        public IActionResult CreateManager()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerCreateModel model)
        {
            AppUser appUser = new AppUser();

            appUser.Name = model.Name;
            appUser.Surname = model.Surname;
            appUser.IdentityNumber = model.IdentityNumber;
            appUser.UserName = model.UserName;
            appUser.PhoneNumber = model.PhoneNumber;
            appUser.Email = model.Email;

            if (model.Password == model.ConfirmPassword)
            {
                var result = await _userManager.CreateAsync(appUser, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(appUser, "Manager");
                    return RedirectToAction("ManagerList");
                }
                else
                {
                    foreach (var item in result.Errors)
                    {
                        ModelState.AddModelError("", item.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError("", "Şifreler Uyuşmuyor");
            }

            return View();

        }

        public async Task <IActionResult> ApartmentList()
        {
            var values = _context.Apartments.ToList();
            return View(values);
        }
        public IActionResult CreateApartment()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateApartment(ApartmentCreateModel model)
        {
            Apartment apartment = new Apartment();

            apartment.Status =Status.Boş;
            apartment.Block = model.Block;
            apartment.DoorNumber = model.DoorNumber;
            apartment.Floor = model.Floor;
            apartment.Block = model.Block;
            apartment.RoomCount = (model.RoomCount);

            var apartments = _apartmentService.TGetList();

            foreach (var item in apartments)
            {
                if (apartment.Block == item.Block && apartment.DoorNumber == item.DoorNumber)
                {
                    TempData["mesaj"] = "Bu daire daha önce eklenmiştir.";
                    return View();
                }
            }
            
            _apartmentService.TAdd(apartment);

            return RedirectToAction("ApartmentList");
        }

        public async Task <IActionResult> UpdateOwner()
        {
           
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateOwner(int id, OwnerCreateModel model)
        {
        //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var user = await _userManager.FindByIdAsync(id.ToString());

            user.UserName = model.UserName;
            user.PhoneNumber=model.PhoneNumber;
            user.Email = model.Email;
            user.LicensePlate = model.LicensePlate;
            
            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("OwnerList", "Apartment");
            }
            return View();


        }

        public async Task<IActionResult> DeleteOwner(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            var apartment = await _context.Apartments.Where(x => x.AppUserID == user.Id).FirstOrDefaultAsync();
            user.StatusUser = StatusUser.Pasif;
            apartment.Status = Status.Boş;

            _context.Update(apartment);
            _context.SaveChanges();

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                return RedirectToAction("OwnerList", "Apartment");
            }
            return View();
        }

        public async Task<IActionResult> UpdateApartment()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> UpdateApartment(int id, ApartmentCreateModel model)
        {
            //    var user = await _userManager.FindByNameAsync(User.Identity.Name);
            var apart = await _context.Apartments.Where(x=>x.AppUserID == id).FirstOrDefaultAsync();
            
            apart.Block = model.Block;
            apart.DoorNumber = model.DoorNumber;
            apart.Floor = model.Floor;
            apart.RoomCount = model.RoomCount;

             _apartmentService.TUpdate(apart);
            
             return RedirectToAction("ApartmentList", "Apartment");

        }

        public async Task <IActionResult> DeleteApartment(int id)
        {
            var apartment = await _context.Apartments.Where(x => x.ApartmentID == id).FirstOrDefaultAsync();
             _apartmentService.TDelete(apartment);
            return RedirectToAction("ApartmentList", "Apartment");
        }

        public IActionResult CreatePDF()
        {
            var values = _context.Apartments.ToList();
            return View(values);
        }
    }
}
