using CarRental.Models;
using Microsoft.AspNetCore.Mvc;
using ProjektCarRental.Data;

namespace CarRental.Data
{
    public class CarRepository : ICar
    {
        private readonly ApplicationDbContext _context;

        public CarRepository (ApplicationDbContext context)
        {
            _context = context;
        }

        public Car Add(Car car)
        {
            car.Available = true; // Sätter bilen som tillgänglig när den läggs till
            _context.Cars.Add(car); // Lägger till bilen i databasen
            _context.SaveChanges();
            return car;
        }

        public void Delete(int id)
        {
            var car = _context.Cars.Find(id); // Hittar bilen med det angivna id:t
            if (car != null)
            {
                _context.Cars.Remove(car); // Tar bort bilen från databasen
                _context.SaveChanges();
            }
        }

        public Car GetCar(int id)
        {
            return _context.Cars.Find(id); // Hittar bilen med det angivna id:t
            
        }

        public IEnumerable<Car> ListAllCars()
        {
            return _context.Cars.Where(c => c.Available).OrderByDescending(c => c.Model).ToList(); // Hämtar alla tillgängliga bilar och sorterar dem efter modell i fallande ordning
        }

        public Car Update(Car car)
        {
            var existingCar = _context.Cars.Find(car.Id);
            if (existingCar != null)
            {
                existingCar.Brand = car.Brand; // Uppdaterar bilens märke
                existingCar.Model = car.Model; // Uppdaterar bilens modell
                existingCar.Available = car.Available; // Uppdaterar bilens tillgänglighet
                _context.SaveChanges(); // Sparar ändringarna i databasen
                return existingCar;
            }
            return null; // Returnerar null om bilen inte hittas //korrekt sätt?
        }
    }
}
