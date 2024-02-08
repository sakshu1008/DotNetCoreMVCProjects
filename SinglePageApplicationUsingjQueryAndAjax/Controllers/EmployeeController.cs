using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SinglePageApplicationUsingjQueryAndAjax.Models;

namespace SinglePageApplicationUsingjQueryAndAjax.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly EmployeeDBContext _context;

        public EmployeeController(EmployeeDBContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var employee = await _context.Employees.ToListAsync();
            if(employee != null)
            {
                return Json(new {data = employee});
            }
                return Json(new {success = false});
        }
        [HttpGet]
        public async Task<IActionResult> GetById(int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                return Json(new { data = employee });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Employee employee )
        {
            if(ModelState.IsValid)
            {
                await _context.Employees.AddAsync(employee);
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateEmployee([FromBody] Employee employee)
        {
            var emp = await _context.Employees.FindAsync(employee.Id);
            if (emp == null)
            {
                return Json(new { success = false });
            }
            if (ModelState.IsValid)
            {
                emp.Salary = employee.Salary;
                emp.Name = employee.Name;
                emp.Department = employee.Department;
                await _context.SaveChangesAsync();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteEmployee(int id)
        {
            var emp = await _context.Employees.FindAsync(id);
            if (emp == null)
            {
                return Json(new { success = false });
            }
            _context.Employees.Remove(emp);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
