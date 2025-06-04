using CarRental.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CarRental.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace CarRental.Controllers
{
    [Authorize(Roles = "Customer")] // Sätter behörighet för hela controllern. 
    public class BookingsController : Controller
    {
        private readonly IBooking _bookingRepository;
        private readonly ICar _carRepository;

        public BookingsController(IBooking bookingRepository, ICar carRepository )
        {
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
        }


        // GET: BookingsController
        
        public ActionResult Index()
        {
            var bookings = _bookingRepository.GetAllBookings(); // Hämtar alla bokningar
            return View("Index", bookings);
        }

      

        // GET: BookingsController/Details/5
        public ActionResult Details(int id)
        {
            var booking = _bookingRepository.GetBookingById(id); // Hämtar bokning med det angivna id:t
            if (booking == null)
            {
                return NotFound();
            }
           return View(booking);
        }

        // GET: BookingsController/Create
        [HttpGet]
        
        public IActionResult Create(int? carId)
        {
            if (!User.Identity.IsAuthenticated) // Om användaren inte är inloggad, spara carId i TempData och omdirigera till inloggningssidan
            {
                TempData["ReturnCarId"] = carId;
                return RedirectToAction("LoginRedirect", "Account");
            }


           var availableCars = _carRepository.ListAllCars(); // Hämtar alla bilar
            ViewBag.Cars = new SelectList(availableCars, "Id", "DisplayName", carId); // Skapar en SelectList med bilarna
            return View(new Booking { CarId = carId ?? 0 });
        }

        // POST: BookingsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public IActionResult Create(Booking booking)
        {
            try
            {
                // Logga för att felsöka
                Console.WriteLine($"CarId: {booking.CarId}, StartDate: {booking.StartDate}, EndDate: {booking.EndDate}");

                // Kontrollera att nödvändiga fält är ifyllda
                if (booking.CarId == 0 || booking.StartDate == default || booking.EndDate == default)
                {
                    TempData["ErrorMessage"] = "Vänligen fyll i alla fält korrekt.";
                    ViewBag.Cars = new SelectList(_carRepository.ListAllCars(), "Id", "DisplayName", booking.CarId);
                    return View(booking);
                }

                _bookingRepository.Create(booking);
                TempData["SuccessMessage"] = "Bokning skapad!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Fel: {ex.Message}");
                TempData["ErrorMessage"] = $"Ett fel uppstod: {ex.Message}";
                ViewBag.Cars = new SelectList(_carRepository.ListAllCars(), "Id", "DisplayName", booking.CarId);
                return View(booking);
            }
                  
        }

        // GET: BookingsController/Edit/5
        //[Authorize(Roles = "Customer")]
        //public ActionResult Edit(int id) // Tas bort???????
        //{
        //    var booking = _bookingRepository.GetBookingById(id);

        //    return View(booking);
        //}

        // POST: BookingsController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        ////[Authorize(Roles = "Customer")]
        //public ActionResult Edit(int id, IFormCollection collection) // Tas bort pga User story???????
        //{
        //    try
        //    {
        //        return RedirectToAction(nameof(Index));
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //GET: BookingsController/Delete/5
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }
            _bookingRepository.DeleteBooking(id); // Tar bort bokning med det angivna id:t
            TempData["Message"] = "Bokning borttagen!"; // Meddelande om att bokningen tagits bort
            return RedirectToAction(nameof(Index));
        }

     

    }
}
