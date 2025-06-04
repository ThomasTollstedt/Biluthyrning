using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Booking
    {
        [Key]
        public int Id { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }

        [Required(ErrorMessage = "Välj startdatum")]
        public DateTime StartDate { get; set; } = DateTime.Today;
        [Required(ErrorMessage = "Välj slutdatum")]
        public DateTime EndDate { get; set; } = DateTime.Today;


    }
}
