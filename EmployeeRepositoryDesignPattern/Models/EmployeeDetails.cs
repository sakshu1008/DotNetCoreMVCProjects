using System.ComponentModel.DataAnnotations;

namespace EmployeeRepositoryDesignPattern.Models
{
    public class EmployeeDetails
    {
        [Key]
        public int EmployeeId {  get; set; }
        [Required]
        public string EmployeeName { get; set; } = string.Empty;
        [Required]
        public string EmployeeDescription { get; set; } = string.Empty;
        [Required]
        public long EmployeePhone { get; set; }
        [Required]
        public bool IsActive {  get; set; }
    }
}
