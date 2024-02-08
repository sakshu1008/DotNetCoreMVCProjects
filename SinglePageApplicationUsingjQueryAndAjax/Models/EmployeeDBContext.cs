using Microsoft.EntityFrameworkCore;

namespace SinglePageApplicationUsingjQueryAndAjax.Models
{
    public class EmployeeDBContext:DbContext
    {
        public EmployeeDBContext(DbContextOptions<EmployeeDBContext> option):base(option)
        {
            
        }
        public DbSet<Employee> Employees { get; set;}
    }
}
