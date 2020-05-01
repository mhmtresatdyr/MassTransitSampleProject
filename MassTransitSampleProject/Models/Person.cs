using System.ComponentModel.DataAnnotations;

namespace MassTransitSampleProject.Models
{
    public class Person
    {

        [Required(ErrorMessage = "Lütfen adınızı giriniz.")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Lütfen soyadınızı giriniz.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email boş bırakılamaz")]
        [RegularExpression(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}" +
                           @"\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\" +
                           @".)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$",
                           ErrorMessage = "Email adresi geçersiz")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Kimlik Numarası bırakılamaz")]
        public string IdentityNumber { get; set; }
    }
}
