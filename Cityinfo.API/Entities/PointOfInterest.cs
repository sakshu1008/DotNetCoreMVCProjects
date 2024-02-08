using System.ComponentModel.DataAnnotations.Schema;

namespace Cityinfo.API.Entities
{
    public class PointofInterest
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        [ForeignKey("CityId")]
        public City? City { get; set; }
        public int CityId { get; set; } 
    }
}
