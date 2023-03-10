using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using SiteYonetim.DataAccess.Context;
using SiteYonetim.Entities.Concrete;
using SiteYonetim.UI.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SiteYonetim.UI
{
    public class Program
    {
        //public async static Task Main(string[] args)
        //{

        //        var host = CreateHostBuilder(args).Build();
        //        using (var scope = host.Services.CreateScope())
        //        {
        //            var services = scope.ServiceProvider;
        //            var context = services.GetRequiredService<SiteManagementDbContext>();
        //            var userManager = services.GetRequiredService<UserManager<AppUser>>();
        //            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        //            await ContextSeed.SeedRolesAsync(userManager, roleManager);
        //            await ContextSeed.SeedAdminAsync(userManager, roleManager);
        //        }
        //        host.Run();

        //}

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();

        }
        public static IHostBuilder CreateHostBuilder(string[] args) =>
           Host.CreateDefaultBuilder(args)
               .ConfigureWebHostDefaults(webBuilder =>
               {
                   webBuilder.UseStartup<Startup>();
               });
        
    }
}
