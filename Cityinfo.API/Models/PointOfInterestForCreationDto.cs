using System.ComponentModel.DataAnnotations;

namespace Cityinfo.API.Models
{
    public class PointOfInterestForCreationDto
    {
        [Required(ErrorMessage = "You should provide a value for name!!!")]
        [MaxLength(15)]
        public string Name { get; set; } = string.Empty;
        [MaxLength(50)]
        public string? Description { get; set; }
    }
}
