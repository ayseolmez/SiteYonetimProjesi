using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Entities.Concrete
{
    public class CreditCard
    {
        [Key]
        public int CardID { get; set; }
        [StringLength(16)]
        public string CardNumber { get; set; }
        [StringLength(5)]
        public string ExpirationDate { get; set; }
        [StringLength(3)]
        public string CVV { get; set; }
        public int Remainder { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
