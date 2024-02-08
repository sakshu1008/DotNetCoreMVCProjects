using Cityinfo.API.Data;
using Cityinfo.API.Models;

namespace Cityinfo.API
{
    public class CitiesDataStore
    {
        private readonly CityDbContext _context;
        public List<CityDto> Cities { get; set; }
        public CitiesDataStore(CityDbContext context, List<CityDto> cities)
        {
            _context = context;
            Cities = cities;
        }

        public static CitiesDataStore Current { get; } = new CitiesDataStore();
        public CitiesDataStore()
        {
            //Cities = _context.City.ToList();
            Cities = new List<CityDto>()
                {
                    new CityDto()
                    {
                        Id = 1, Name = "New York City", Description = "Famous for Times of square", PointOfInterests = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 1,
                                Name = "Central Park",
                                Description = "Most Visited Site"
                            },
                            new PointOfInterestDto()
                            {
                                Id = 2,
                                Name = "Empire State building",
                                Description = "One of the tallest building"
                            }
                        }
                    },
                    new CityDto()
                    {
                        Id = 2, Name = "Pune", Description = "City of Education", PointOfInterests = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 3,
                                Name = "JM Road",
                                Description = "Famous for street shopping"
                            },
                            new PointOfInterestDto()
                            {
                                Id = 4,
                                Name = "Shaniwar Wada",
                                Description = "Historical place"
                            }
                        }
                    },
                    new CityDto()
                    {
                        Id = 3, Name = "Amserdam", Description = "Tulip Gardens", PointOfInterests = new List<PointOfInterestDto>()
                        {
                            new PointOfInterestDto()
                            {
                                Id = 5,
                                Name = "Wind Wheel",
                                Description = "Most visited site"
                            },
                            new PointOfInterestDto()
                            {
                                Id = 6,
                                Name = "Snow peacked mountains",
                                Description = "One of the beautiful place"
                            }
                        }
                    }
                };
        }        
    }
}
