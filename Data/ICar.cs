using CarRental.Models;

namespace CarRental.Data
{
    public interface ICar
    {
        Car Add(Car car); // Lägger till en bil
        void Delete(int id); // Tar bort en bil

        Car GetCar(int id); // Hämtar en bil

        Car Update(Car car); // Uppdaterar en bil

        IEnumerable<Car> ListAllCars(); // Lista alla bilar

        


    }
}
