using SiteYonetim.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SiteYonetim.Entities.Concrete
{
    public class Apartment
    {
        [Key]
        public int ApartmentID { get; set; }
        public Block Block { get; set; }
        public Status Status { get; set; }
        public OwnerType? Owner { get; set; }
        public int RoomCount { get; set; }
        public string Floor { get; set; }
        public string DoorNumber { get; set; }
        public virtual int? AppUserID { get; set; }
        public AppUser AppUser { get; set; }


    }
}
