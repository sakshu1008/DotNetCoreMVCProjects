using AutoMapper;
using EmployeeWEBAPI.Data;
using EmployeeWEBAPI.Models;
using EmployeeWEBAPI.Models.DTO;
using EmployeeWEBAPI.Services.IServices;
using Microsoft.EntityFrameworkCore;

namespace EmployeeWEBAPI.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        private readonly IMapper _mapper;
        public EmployeeRepository(EmployeeDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto)
        {
            if(employeeCreateDto == null)
            {
                throw new Exception("Add Employee page not found!!! Please try again.");
            }
            Employee emp = _mapper.Map<Employee>(employeeCreateDto);
            await _context.EmployeesAPI.AddAsync(emp);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteEmployeeAsync(int id)
        {
            if (id == null)
            {
                throw new Exception("Delete Employee page not found!!! Please try again.");
            }
            var EmployeeById = await _context.EmployeesAPI.FindAsync(id);
            Employee EmployeeToDelete = _mapper.Map<Employee>(EmployeeById);
            if(EmployeeToDelete == null)
            {
                throw new Exception("Employee to delete not found!!! Please try again.");
            }
            _context.EmployeesAPI.Remove(EmployeeToDelete);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<EmployeeDto>> GetAllAsync()
        {
            IEnumerable<Employee> EmployeeList = await _context.EmployeesAPI.ToListAsync();
            if(EmployeeList == null)
            {
                throw new Exception("Employee list not found!!! please try again.");
            }
            return _mapper.Map<IEnumerable<EmployeeDto>>(EmployeeList);
        }

        public async Task<EmployeeDto> GetByIdAsync(int id)
        {
            if(id == null)
            {
                throw new Exception("Get employee by Id page not found!!! please try again.");
            }
            var EmployeeById = await _context.EmployeesAPI.FindAsync(id);
            return _mapper.Map<EmployeeDto>(EmployeeById);
        }

        public async Task<int> UpdateEmployeeAsync(EmployeeUpdateDto employeeUpdateDto)
        {
            var EmployeeToUpdate = await _context.EmployeesAPI.FirstOrDefaultAsync(x => x.Id == employeeUpdateDto.Id);
            if(EmployeeToUpdate == null)
            {
                throw new Exception("Update employee page not found!!! Please try again...");
            }
            else
            {
                EmployeeToUpdate.Name = employeeUpdateDto.Name;
                EmployeeToUpdate.Email = employeeUpdateDto.Email;
                EmployeeToUpdate.Salary = employeeUpdateDto.Salary;
                EmployeeToUpdate.Location = employeeUpdateDto.Location;
                EmployeeToUpdate.Department = employeeUpdateDto.Department;
                EmployeeToUpdate.Phone = employeeUpdateDto.Phone;

                var updatedEmployee = _mapper.Map<Employee>(EmployeeToUpdate);
                _context.EmployeesAPI.Update(updatedEmployee);
                return await _context.SaveChangesAsync();
            }
        }
    }
}
