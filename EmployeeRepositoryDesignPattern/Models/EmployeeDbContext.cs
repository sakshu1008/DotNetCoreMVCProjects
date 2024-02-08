using Microsoft.EntityFrameworkCore;

namespace EmployeeRepositoryDesignPattern.Models
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options):base(options)
        {
                
        }
        public DbSet<EmployeeDetails> EmployeeDetails { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeDetails>().HasData(
                new EmployeeDetails
                {
                    EmployeeId = 1,
                    EmployeeName="Sakshi Modase",
                    EmployeeDescription=".NET Developer",
                    EmployeePhone=8530601006,
                    IsActive=true
                },
                new EmployeeDetails
                {
                    EmployeeId = 2,
                    EmployeeName = "Saurabh Modase",
                    EmployeeDescription = "Business Analyst",
                    EmployeePhone = 7387481003,
                    IsActive = true
                }
                );
        }
    }
}
