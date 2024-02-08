using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CompanyName {  get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime LaunchDate { get; set; }
        public DateTime LastUpdated { get; set; }

    }
}
