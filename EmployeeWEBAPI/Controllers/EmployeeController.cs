using AutoMapper;
using EmployeeWEBAPI.Models;
using EmployeeWEBAPI.Models.DTO;
using EmployeeWEBAPI.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeWEBAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployee()
        {
            return Ok(await _employeeRepository.GetAllAsync());
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> GetEmployee(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _employeeRepository.GetByIdAsync(id));  
        }

        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployee([FromBody] EmployeeCreateDto model)
        {
            var EmployeeList = await _employeeRepository.GetAllAsync();
            var EmployeeExists = EmployeeList.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower());
            if(EmployeeExists != null)
            {
                ModelState.AddModelError("CustomMessage", $"Employee already exists");
                return BadRequest();
            }
            if(model == null)
            {
                return BadRequest(model);
            }
            return Ok(await _employeeRepository.AddEmployeeAsync(model));
        }
        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployee([FromBody] EmployeeUpdateDto model, [FromRoute] int id)
        {
            Employee EmployeeById = _mapper.Map<Employee>(await _employeeRepository.GetByIdAsync(id));
            if (EmployeeById == null)
            {
                return BadRequest(EmployeeById);
            }
            if (model == null)
            {
                return BadRequest(model);
            }
            model.Id = id;
            return Ok(await _employeeRepository.UpdateEmployeeAsync(model));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<EmployeeDto>> DeleteEmployee([FromRoute] int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _employeeRepository.DeleteEmployeeAsync(id));
        }
    }
}
