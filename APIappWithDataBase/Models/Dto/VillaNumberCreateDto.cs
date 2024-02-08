using System.ComponentModel.DataAnnotations;

namespace APIappWithDataBase.Models
{
    public class VillaNumberCreateDto
    {
        [Required]
        public int VillaNo { get; set; }
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
