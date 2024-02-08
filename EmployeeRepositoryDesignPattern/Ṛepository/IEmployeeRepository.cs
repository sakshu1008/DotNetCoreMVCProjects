using EmployeeRepositoryDesignPattern.Models;

namespace EmployeeRepositoryDesignPattern.Ṛepository
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<EmployeeDetails>> GetEmployeeDetails();
        Task<EmployeeDetails> GetEmployeeDetailsById(int id);
        Task<int> CreateEmployee(EmployeeDetails employee);
        Task<int> UpdateEmployee(EmployeeDetails employee);
        Task<int> DeleteEmployee(int id);
    }
}
