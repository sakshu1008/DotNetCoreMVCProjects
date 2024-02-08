using EmployeeRepositoryDesignPattern.Models;
using EmployeeRepositoryDesignPattern.Ṛepository;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace EmployeeRepositoryDesignPattern.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeController(IEmployeeRepository employeeRepository)
        {
                _employeeRepository = employeeRepository;
        }
        public async Task<IActionResult> Index()
        {
            var employeeList = await _employeeRepository.GetEmployeeDetails();
            if (employeeList == null)
            {
                return NotFound();
            }
            return View(employeeList);
        }

        public async Task<IActionResult> Details(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employeeDetails = await _employeeRepository.GetEmployeeDetailsById(id);
            if (employeeDetails == null)
            {
                return NotFound();
            }
            return View(employeeDetails);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmployeeDetails employee)
        {
            if (employee == null)
            {
                return BadRequest();
            }
            await _employeeRepository.CreateEmployee(employee);
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var employeeToUpdate = await _employeeRepository.GetEmployeeDetailsById(id);
            if(employeeToUpdate == null)
            {
                return NotFound();
            }
            return View(employeeToUpdate);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EmployeeDetails employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _employeeRepository.UpdateEmployee(employee);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(employee);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var employeeToDelete = await _employeeRepository.GetEmployeeDetailsById(id);
            if (employeeToDelete == null)
            {
                return NotFound();
            }
            return View(employeeToDelete);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (id == null)
            {
                return NotFound();
            }
            await _employeeRepository.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
