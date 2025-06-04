using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using CarRental.Data;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using ProjektCarRental.Models;

namespace CarRental.Controllers
{ 
    [Authorize(Roles = "Admin")] // Endast users med rollen "admin" kommer åt denna controller. 
   
    public class AdminController : Controller
    {
        private readonly IBooking _bookingRepository; // Använder IBooking för att hämta bokningar
        private readonly ICar _carRepository; // Använder ICar för att hämta bilar
        private readonly UserManager<IdentityUser> _usermanager; // Använder UserManager för att hantera användare

        public AdminController(IBooking bookingRepository, ICar carRepository, UserManager<IdentityUser> usermanager)
        {
            _bookingRepository = bookingRepository;
            _carRepository = carRepository;
            _usermanager = usermanager;
        }



        // GET: AdminController
        public async Task<IActionResult> Index()
        {
         var customers = await _usermanager.GetUsersInRoleAsync("Customer"); // Hämtar alla kunder 

            var bookings = _bookingRepository.GetAllBookings();   // Hämtar alla bokningar
            var cars = _carRepository.ListAllCars(); // Hämtar alla bilar

            var viewModel = new AdminDashboardViewModel // Skapar en ViewModel för Admin Dashboard för att visa kunder och bokningar
            {
                Customers = customers,
                Bookings = bookings,
                Cars = cars

            };

            return View(viewModel);
        }

        // GET: AdminController/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        // GET: AdminController/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        // POST: AdminController/Create
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create(IFormCollection collection)
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

        // GET: AdminController/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        // POST: AdminController/Edit/5
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: AdminController/Delete/5
        [HttpGet]
        public IActionResult Delete(int id)
        {
            var booking = _bookingRepository.GetBookingById(id);
            if (booking == null)
            {
                return NotFound();
            }

            if (booking.StartDate >= DateTime.Now)
            {
                _bookingRepository.DeleteBooking(id);
                TempData["BookingRemoved"] = "Bokning borttagen!"; // Meddelande om att bokningen tagits bort
            }
            else
            {
                TempData["BookingRemoved"] = "Bokningen kan inte tas bort, slutdatum har passerat.";
            
            }
                return RedirectToAction(nameof(Index));
            //return View();
        }

        // POST: AdminController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public IActionResult CreateUser()
        {
            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateUser(CreateUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser { UserName = model.Email, Email = model.Email };
                var result = await _usermanager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(user, "Customer"); // Lägger till användaren i rollen "Customer"
                    TempData["customerCreated"] = "Kund skapad!";
                    return RedirectToAction(nameof(Index));
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

            }
            return View(model);

        }
    }
}
