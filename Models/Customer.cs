using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ProjektCarRental.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

    }
}
