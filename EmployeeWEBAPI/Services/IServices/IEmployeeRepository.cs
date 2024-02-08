using EmployeeWEBAPI.Models.DTO;

namespace EmployeeWEBAPI.Services.IServices
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDto>> GetAllAsync();
        Task<EmployeeDto> GetByIdAsync(int id);
        Task<int> AddEmployeeAsync(EmployeeCreateDto employeeCreateDto);
        Task<int> DeleteEmployeeAsync(int id);
        Task<int> UpdateEmployeeAsync(EmployeeUpdateDto employeeUpdateDto);
    }
}
