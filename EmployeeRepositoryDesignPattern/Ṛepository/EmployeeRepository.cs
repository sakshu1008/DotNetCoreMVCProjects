using EmployeeRepositoryDesignPattern.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeRepositoryDesignPattern.Ṛepository
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;

        public EmployeeRepository(EmployeeDbContext context)
        {
            _context = context;
        }

        public async Task<int> CreateEmployee(EmployeeDetails employee)
        {
            if(employee == null)
            {
                throw new Exception($"Add Employee Not Found!!! Please try again");
            }
            await _context.EmployeeDetails.AddAsync( employee );
            return await _context.SaveChangesAsync(); 
        }

        public async Task<int> DeleteEmployee(int id)
        {
            if(id == null)
            {
                throw new Exception($"Delete Employee not found!!! Please try again");
            }
            var EmployeeToDelete = await _context.EmployeeDetails.FindAsync(id);
            if(EmployeeToDelete == null)
            {
                throw new Exception($"Employee data not found with id: "+id);
            }
            _context.EmployeeDetails.Remove(EmployeeToDelete);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeDetails>> GetEmployeeDetails()
        {
            return await _context.EmployeeDetails.ToListAsync();
        }

        public async Task<EmployeeDetails> GetEmployeeDetailsById(int id)
        {
            if(id == null)
            {
                throw new Exception($"Get Employees by Id Not Found!!! Please try again");
            }
            var EmployeeById = await _context.EmployeeDetails.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if(EmployeeById == null)
            {
                throw new Exception($"Employee data Not found with id: "+ id);
            }
            return EmployeeById;
        }

        public async Task<int> UpdateEmployee(EmployeeDetails employee)
        {
            if(employee == null)
            {
                throw new Exception($"Update Employee Not found!!! Please try again");
            }
            _context.Entry(employee).State = EntityState.Modified;
            return await _context.SaveChangesAsync();
        }
    }
}
