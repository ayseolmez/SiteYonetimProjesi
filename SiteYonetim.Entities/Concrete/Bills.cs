using SiteYonetim.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Entities.Concrete
{
    public class Bills
    {
        [Key]
        public int BillID { get; set; }
        public int Amount { get; set; }
        public State State { get; set; }
        public DateTime Date { get; set; }
        public int AppUserID { get; set; }
        public AppUser AppUsers { get; set; }
    }
}
