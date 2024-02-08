using EmployeeWEBAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWEBAPI.Data
{
    public class EmployeeDbContext:DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options):base(options)
        {
            
        }

        public DbSet<Employee> EmployeesAPI { get; set; }
    }
}
