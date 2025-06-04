using CarRental.Controllers;
using CarRental.Models;
using Microsoft.EntityFrameworkCore;
using ProjektCarRental.Data;

namespace CarRental.Data
{
    public class BookingRepository : IBooking
    {
        private readonly ApplicationDbContext _context;

        public BookingRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public Booking Create(Booking booking)
        {
            _context.Bookings.Add(booking);
            var car = _context.Cars.Find(booking.CarId);
            if (car != null) // Logik för att se till att en bil inte är tillgänglig vid bokning.
            {
                car.Available = false; // Markera bilen som ej tillgänglig
            }
            _context.SaveChanges();
            return booking;
            
        }

        public void DeleteBooking(int id)
        {
            var booking = _context.Bookings.Find(id); // Hitta bokning från Db och tillskriv booking
            if (booking != null)
            {
                var car = _context.Cars.Find(booking.CarId);    //Hämtar bil genom Id som kopplas till kundens Id.                            
                if (car != null)
                {
                    car.Available = true; // Markera bilen som tillgänglig igen
                }
                if (booking.StartDate.Date >= DateTime.Now.Date) // Logik för att endast ta bort kommande bokningar
                {
                     _context.Bookings.Remove(booking);
                    
                }
                   
                _context.SaveChanges();
            }
        }

        public IEnumerable<Booking> GetAllBookings() //Hämtar bokningar och motverkar lazy loading med .Include för att få med alla bilar
        {
            return _context.Bookings.Include(b => b.Car)
               .OrderByDescending(b => b.Id).ToList();                           
               
        }

        public Booking GetBookingById(int id) // Hämtar en bokning, motverkar lazy loading m .Include för att få med bilen kopplat till bokningen.
        {
            var booking = _context.Bookings.Include(b => b.Car).FirstOrDefault(b => b.Id == id);
            if (booking != null) 
            {
                return booking;
            }
            return null; 
        }
    }
}
