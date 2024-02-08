using APIappWithDataBase.Models;
using Microsoft.EntityFrameworkCore;

namespace APIappWithDataBase.Data
{
    public class VillaDbContext : DbContext
    {
        public VillaDbContext(DbContextOptions<VillaDbContext> options) : base(options)
        {

        }
        public DbSet<Villa> Villas { get; set; }
        public DbSet<VillaNumber> VillaNumbers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa
                {
                    Id = 1,
                    Name = "Royal Villa",
                    Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    ImageUrl = "",
                    Occupancy = 5,
                    Rate = 250,
                    Sqft = 600,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 2,
                    Name = "Row house Villa",
                    Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    ImageUrl = "",
                    Occupancy = 7,
                    Rate = 400,
                    Sqft = 1200,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                },
                new Villa
                {
                    Id = 3,
                    Name = "pool Villa",
                    Details = "Lorem Ipsum is simply dummy text of the printing and typesetting industry.",
                    ImageUrl = "",
                    Occupancy = 3,
                    Rate = 600,
                    Sqft = 600,
                    Amenity = "",
                    CreatedDate = DateTime.Now
                }
                );
            base.OnModelCreating(modelBuilder);
        }
    }
}
