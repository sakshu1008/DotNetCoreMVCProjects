using Cityinfo.API.Data;

namespace Cityinfo.API.Models
{
    public static class ModelBuilderExtension
    {
        public static void Seed(CityDbContext context)
        {
            if (!context.City.Any())
            {
                context.AddRange(new CityDto
                {
                    Id = 1,
                    Name = "Mumbai",
                    Description = "Gateway of India",
                    PointOfInterests = new List<PointOfInterestDto>
                    {
                        new PointOfInterestDto()
                        {
                            Id = 1,
                            Name = "Marine Drive",
                            Description = "Sea view"
                        }
                    }
                });
                context.SaveChanges();
            }
        }
    }
}
