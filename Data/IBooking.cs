using CarRental.Controllers;
using CarRental.Models;

namespace CarRental.Data
{
    public interface IBooking
    {
      
        IEnumerable<Booking> GetAllBookings(); // Lista alla tidigare bokningar

        Booking GetBookingById(int id); // Hämta bokning med id

        Booking Create(Booking booking);

        /*Booking UpdateBooking(Booking booking);*/ // Tas bort pga ej i User Stories?

        void DeleteBooking(int id);



    }
}
