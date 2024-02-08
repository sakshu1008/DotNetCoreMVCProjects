using APIappWithDataBase.Data;
using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;
using APIappWithDataBase.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIappWithDataBase.Controllers
{
    [Route("api/villa")]
    [ApiController]
    public class VillaController : ControllerBase
    {
        private readonly VillaDbContext _dbContext;
        private readonly IMapper _mapper;
        private readonly IVillaRepository _villaRepository;
        public VillaController(VillaDbContext dbContext, IMapper mapper, IVillaRepository villaRepository)
        {
            _dbContext = dbContext;
            _mapper = mapper;
            _villaRepository = villaRepository;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<VillaDto>>> GetVillasAsync()
        {
            return Ok(await _villaRepository.GetAll());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<VillaDto>> GetVillaByIdAsync(int id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            return Ok(await _villaRepository.GetVillaByIdasync(id));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDto>> AddVilaaAsync([FromBody] VillaCreateDto villaCreateDto)
        {
            
            var villaExists = await _dbContext.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == villaCreateDto.Name.ToLower());
            if (villaExists != null)
            {
                ModelState.AddModelError("CustomErrorMsg",$"Villa already exists!");
                return BadRequest();
            }
            
            if (villaCreateDto == null)
            {
                return BadRequest(villaCreateDto);
            }

            return Ok(await _villaRepository.AddVilla(villaCreateDto));


            // WITH AUTOMAPPER
            //Villa model = _mapper.Map<Villa>(villaCreateDto);

            // WITHOUT AUTOMAPPER
            //Villa model = new()
            //{
            //    Name = model.Name,
            //    Details = model.Details,
            //    Rate = model.Rate,
            //    Sqft = model.Sqft,
            //    Occupancy  = model.Occupancy,
            //    ImageUrl = model.ImageUrl,
            //    Amenity = model.Amenity
            //};
            //await _dbContext.Villas.AddAsync(model);
            //await _dbContext.SaveChangesAsync();
            //return CreatedAtRoute("Get Villa", new { Id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VillaDto>> UpdateVillaAsync([FromRoute] int id, VillaUpdateDto villaUpdateDto)
        {
            Villa model = _mapper.Map<Villa>(await _villaRepository.GetVillaByIdasync(id));

            if (model == null)
            {
                return BadRequest(model);
            }
            if(model.Name != villaUpdateDto.Name)
            {
                var villaExists = await _dbContext.Villas.FirstOrDefaultAsync(v => v.Name.ToLower() == villaUpdateDto.Name.ToLower());
                if (villaExists != null)
                {
                    ModelState.AddModelError("CustomErrorMsg", $"Villa already exists!");
                    return BadRequest();
                }
            }
            villaUpdateDto.Id = id;
            //return CreatedAtRoute("Get Villa", new { Id = model.Id }, model);
            return Ok(await _villaRepository.UpdateVilla(villaUpdateDto));
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<VillaDto>> DeleteVillaAsync(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            return Ok(await _villaRepository.DeleteVilla(id));
        }
    }
}
