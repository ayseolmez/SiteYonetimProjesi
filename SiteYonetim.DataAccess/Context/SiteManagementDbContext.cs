using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiteYonetim.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.DataAccess.Context
{
    public class SiteManagementDbContext : IdentityDbContext<AppUser,AppRole,int>
    {
        public SiteManagementDbContext(DbContextOptions<SiteManagementDbContext> options) : base(options)
        {

        }
        public DbSet<Apartment> Apartments { get; set; }    
        public DbSet<Bills> Bills { get; set; }    
        public DbSet<Dues> Dues { get; set; }    
        public DbSet<Message> Messages { get; set; }    
        public DbSet<CreditCard> CreditCards { get; set; }

       
        
    }
}
