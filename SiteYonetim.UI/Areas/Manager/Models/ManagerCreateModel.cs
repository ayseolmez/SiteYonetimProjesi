using System.ComponentModel.DataAnnotations;

namespace SiteYonetim.UI.Areas.Manager.Models
{
	public class ManagerCreateModel
	{
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MinLength(2, ErrorMessage = "En az 2 karakter uzunluğunda olmalıdır.")]
        public string Surname { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(11)]
        public string IdentityNumber { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [MinLength(10, ErrorMessage = "En az 10 karakter uzunluğunda olmalıdır.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(5)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        [StringLength(5)]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "Bu alan boş geçilemez")]
        public string Email { get; set; }
    }
}
