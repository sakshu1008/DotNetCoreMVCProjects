using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VehicleAPI.Models;
using VehicleAPI.Models.DTO;
using VehicleAPI.Services.IServices;

namespace VehicleAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleRepository _vehicleRepository;
        private readonly IMapper _mapper;

        public VehicleController(IVehicleRepository vehicleRepository,IMapper mapper)
        {
            _vehicleRepository = vehicleRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleDto>>> GetVehicle()
        {
            return Ok(await _vehicleRepository.GetAllAsync());
        }
        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<VehicleDto>> GetVehicleById([FromRoute] int id)
        {
           if(id == null)
            {
                return BadRequest();
            }
           var VehicleById = await _vehicleRepository.GetByIdAsync(id);
           if(VehicleById == null)
            {
                return BadRequest();
            }
            return Ok(_mapper.Map<VehicleDto>(VehicleById));
        }

        [HttpPost]
        public async Task<ActionResult<VehicleDto>> AddVehicle([FromBody] CreateVehicleDto model)
        {
            var VehicleList = await _vehicleRepository.GetAllAsync();
            var VehicleExits = VehicleList.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower());
            if(VehicleExits != null)
            {
                ModelState.AddModelError("CustomMessage", $"Vehicle already exists");
                return BadRequest();
            }
            if(model == null)
            {
                return BadRequest();
            }
            return Ok(await _vehicleRepository.CreateVehicleAsync(model));  
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<ActionResult<VehicleDto>> UpdateVehicle( [FromBody] UpdateVehicleDto model, [FromRoute] int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            Vehicle VehicleToUpdate = _mapper.Map<Vehicle>(await _vehicleRepository.GetByIdAsync(id));
            if(VehicleToUpdate == null)
            {
                return NotFound();
            }
            if (model == null)
            {
                return BadRequest();
            }
            model.Id = id;
            return Ok(await _vehicleRepository.UpdateVehicleAsync(model));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<ActionResult<VehicleDto>> DeleteVehicle([FromRoute] int id)
        {
            if(id == null)
            {
                return BadRequest();
            }
            return Ok(await _vehicleRepository.DeleteVehicleAsync(id));
        }
    }
}
