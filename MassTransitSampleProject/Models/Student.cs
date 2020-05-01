using System.ComponentModel.DataAnnotations;

namespace MassTransitSampleProject.Models
{
    public class Student
    {
        public Person PersonDetails { get; set; }
        [Required(ErrorMessage = "Lütfen sınıfını giriniz.")]
        public string ClassRoom { get; set; }

        [Required(ErrorMessage = "Lütfen okul numarasını giriniz.")]
        [Range(1, 100, ErrorMessage = "Numara 1-100 arasında olmalıdır.")]
        public int SchoolNumber { get; set; }
    }
}
