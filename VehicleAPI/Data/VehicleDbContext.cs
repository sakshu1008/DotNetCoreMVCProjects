using Microsoft.EntityFrameworkCore;
using VehicleAPI.Models;

namespace VehicleAPI.Data
{
    public class VehicleDbContext:DbContext
    {
        public VehicleDbContext(DbContextOptions<VehicleDbContext> option):base(option)
        {
            
        }
        public DbSet<Vehicle> Vehicles { get; set; }
    }
}
