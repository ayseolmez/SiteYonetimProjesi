using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System.Linq;
using SiteYonetim.DataAccess.Context;

namespace SiteYonetim.UI.Data
{
    public static class ContextSeed
    {
        

        //public static async Task SeedApartment(Apartment apartment)
        //{

        //    var apartment1 = new Apartment()
        //    {
        //        Block = Block.A,
        //        Status =Status.Boş,
        //        RoomCount=1,
        //        Floor= "1",
        //        DoorNumber= "2",
        //    };

            
        //}

        public static async Task SeedRolesAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Seed Roles
            await roleManager.CreateAsync(new IdentityRole(Roles.Owner.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Manager.ToString()));
        }

        public static async Task SeedAdminAsync(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            var admin1 = new AppUser
            {
                Name = "Çınar",
                Surname = "Ölmez",
                IdentityNumber = "10101010101",
                PhoneNumber = "05322210902",
                UserName = "admin1",
                Email = "admin1@gmail.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,


            };
            if (userManager.Users.All(userManager => userManager.Id != admin1.Id))
            {
                var user = await userManager.FindByNameAsync(admin1.Name);

                if (user == null)
                {
                    await userManager.CreateAsync(admin1, "Admin123.");
                    await userManager.AddToRoleAsync(admin1, Roles.Manager.ToString());
                }
            }
        }
    }
}
