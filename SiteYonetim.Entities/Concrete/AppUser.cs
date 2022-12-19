using Microsoft.AspNetCore.Identity;
using SiteYonetim.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Entities.Concrete
{
    public class AppUser : IdentityUser<int>
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string IdentityNumber { get; set; }
        public string? LicensePlate { get; set; }
        public StatusUser? StatusUser { get; set; }

        public List<CreditCard>? Cards { get; set; }
        public List<Apartment>? Apartment { get; set; }
        public List<Bills>? Bills { get; set; }
        public List<Dues>? Dues { get; set; }

    }
}
