using SiteYonetim.Entities.Concrete;
using SiteYonetim.Entities.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace SiteYonetim.UI.Areas.Manager.Models
{
    public class OwnerCreateModel
    {
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(11)]
        public string IdentityNumber { get; set; }
        public string? LicensePlate { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(13)]
        public string PhoneNumber { get; set; }
        public StatusUser StatusUser { get; set; } = StatusUser.Aktif;
        public Block Block { get; set; }
        public Status Status { get; set; } = Status.Dolu;
        public OwnerType Owner { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MaxLength(20, ErrorMessage = "En fazla 20 karakter uzunluğunda olmalıdır.")]

        public string DoorNumber { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Email { get; set; }

    }
}
