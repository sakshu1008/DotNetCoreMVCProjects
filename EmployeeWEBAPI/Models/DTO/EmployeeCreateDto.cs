using System.ComponentModel.DataAnnotations;

namespace EmployeeWEBAPI.Models.DTO
{
    public class EmployeeCreateDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public long Phone { get; set; }
        [Required]
        public long Salary { get; set; }
        [Required]
        public string Department { get; set; }
        public string Location { get; set; }
    }
}
