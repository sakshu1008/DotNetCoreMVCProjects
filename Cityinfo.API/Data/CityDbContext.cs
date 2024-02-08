using Cityinfo.API.Entities;
using Cityinfo.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Cityinfo.API.Data
{
    public class CityDbContext:DbContext
    {
        public CityDbContext(DbContextOptions options):base(options) 
        {
                
        }
        public DbSet<City> City { get; set; }
        public DbSet<PointofInterest> PointofInterest { get; set;}
    }
}
