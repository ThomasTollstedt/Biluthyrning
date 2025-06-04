using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Admin
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="Du måste mata in en email!")]
        public string Email { get; set; } // Används för att logga in
        [Required(ErrorMessage = "Du måste mata in ett lösenord!")]
        public string Password { get; set; } // Används för att logga in

        public bool IsAdmin { get; set; } = true; // validering om adminegenskaper skall tilldelas user
    }
}
