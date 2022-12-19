using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetim.UI.Areas.Manager.Models
{
    public class ApartmentCreateModel
    {
        public Block Block { get; set; }
        public Status Status { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public int RoomCount { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Floor { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]
        public string DoorNumber { get; set; }

    }
}
