using System.ComponentModel.DataAnnotations;

namespace VehicleAPI.Models.DTO
{
    public class UpdateVehicleDto
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Description { get; set; }
        [Required]
        public DateTime LaunchDate { get; set; }
    }
}
