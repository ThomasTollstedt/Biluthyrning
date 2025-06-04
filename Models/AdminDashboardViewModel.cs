using CarRental.Models;
using Microsoft.AspNetCore.Identity;

namespace ProjektCarRental.Models
{
    public class AdminDashboardViewModel
    {
        public IEnumerable<IdentityUser> Customers { get; set; }
        public IEnumerable<Booking> Bookings { get; set; }

        public IEnumerable<Car> Cars { get; set; } 
    }
}
