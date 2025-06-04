using System.ComponentModel.DataAnnotations;

namespace CarRental.Models
{
    public class Car
    {
        public int Id { get; set; }
        public  string Brand { get; set; }
        public string? Model { get; set; }
        public bool Available { get; set; } = true; // Sätter alla bilar som läggs till som tillgängliga initialt?
        public string DisplayName => $"{Brand} {Model}"; // Visar bilens märke och modell i en sträng

        public string ImageUrl { get; set; }
    }
}
