using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;
using APIappWithDataBase.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIappWithDataBase.Controllers
{
    [Route("api/villas")]
    [ApiController]
    public class VillasController : ControllerBase
    {
        private readonly ILogger<VillasController> _logger;
        private readonly IMapper _mapper;
        private readonly IVillasRepository _villasRepository;
        protected APIResponse _apiResponse;

        public VillasController(ILogger<VillasController> logger, IMapper mapper,
            IVillasRepository villasRepository)
        {
            _logger = logger;
            _mapper = mapper;
            _villasRepository = villasRepository;
            this._apiResponse = new APIResponse();
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillasAsync()
        {
            try
            {
                IEnumerable<Villa> villaList = await _villasRepository.GetAllAsync();
                _apiResponse.Result = _mapper.Map<VillaDto>(villaList);
                _apiResponse.StatusCode = HttpStatusCode.OK;
                return Ok(_apiResponse);
            }
            catch (Exception ex)
            {
                _apiResponse.IsSuccess = false;
                _apiResponse.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _apiResponse;
        }

        //public async Task<ActionResult<IEnumerable<Villa>>> GetVillasAsync()
        //{
        //    IEnumerable<Villa> villaList = await _villasRepository.GetAllAsync();
        //    return Ok(_mapper.Map<VillaDto>(villaList));
        //}
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<VillaDto>> GetVillaByIdAsync(int id)
        {
            if (id == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }
            var villaById = await _villasRepository.GetByIdAsync(v => v.Id == id, tracked: false);
            if (villaById == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_apiResponse);
            }
            _apiResponse.Result = _mapper.Map<Villa>(villaById);
            _apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);
        }
        
        //[HttpGet("{id:int}")]
        //public async Task<ActionResult<VillaDto>> GetVillaByIdAsync(int id)
        //{
        //    if (id == null)
        //    {
        //        return BadRequest();
        //    }
        //    var model = await _villasRepository.GetByIdAsync(v => v.Id == id,tracked:false);
        //    return Ok(model);
        //}


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddVilaaAsync([FromBody] VillaCreateDto villaCreateDto)
        {
            var villasList = await _villasRepository.GetAllAsync();
            var villaExists = villasList.FirstOrDefault(x => x.Name.ToLower() == villaCreateDto.Name.ToLower());
            if (villaExists != null)
            {
                ModelState.AddModelError("CustomErrorMsg", $"Villa already exists!");
                return BadRequest();
            }

            if (villaCreateDto == null)
            {
                return BadRequest(villaCreateDto);
            }
            Villa model = _mapper.Map<Villa>(villaCreateDto);
            await _villasRepository.CreateAsync(model);
            _apiResponse.Result = _mapper.Map<VillaDto>(model);
            _apiResponse.StatusCode = HttpStatusCode.Created;
            // return Ok(_apiResponse);
            return CreatedAtRoute("Get Villa", new { Id = model.Id }, _apiResponse);
        }

        //[HttpPost]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<VillaDto>> AddVilaaAsync([FromBody] VillaCreateDto villaCreateDto)
        //{
        //    var villasList = await _villasRepository.GetAllAsync();
        //    var villaExists = villasList.FirstOrDefault(x => x.Name.ToLower() == villaCreateDto.Name.ToLower());
        //    if (villaExists != null)
        //    {
        //        ModelState.AddModelError("CustomErrorMsg", $"Villa already exists!");
        //        return BadRequest();
        //    }

        //    if (villaCreateDto == null)
        //    {
        //        return BadRequest(villaCreateDto);
        //    }
        //    Villa model = _mapper.Map<Villa>(villaCreateDto);
        //    await _villasRepository.CreateAsync(model);
        //    return Ok(model);
        //}

        [HttpDelete("{id:int}", Name = "DeleteVillaAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaAsync(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villaDelete = await _villasRepository.GetByIdAsync(v => v.Id == id);
            if(villaDelete == null)
            {
                return NotFound();
            }
            await _villasRepository.RemoveAsync(villaDelete);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }

        //[HttpDelete("{id:int}", Name = "DeleteVillaAsync")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<VillaDto>> DeleteVillaAsync(int id)
        //{
        //    if (id == 0)
        //    {
        //        return BadRequest();
        //    }
        //    var villaDelete = await _villasRepository.GetByIdAsync(v => v.Id == id);
        //    if (villaDelete == null)
        //    {
        //        return NotFound();
        //    }
        //    await _villasRepository.RemoveAsync(villaDelete);
        //    return NoContent();
        //}

        [HttpPut("{id:int}", Name = "UpdateVillaAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaAsync(int id, [FromBody]VillaUpdateDto villaUpdateDto)
        {
            if(id != villaUpdateDto.Id || villaUpdateDto == null)
            {
                return BadRequest();
            }
            Villa model = _mapper.Map<Villa>(villaUpdateDto);
            await _villasRepository.UpdateAsync(model);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsSuccess = true;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(_apiResponse);
        }

        //[HttpPut("{id:int}", Name = "UpdateVillaAsync")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task<ActionResult<VillaDto>> UpdateVillaAsync(int id, [FromBody] VillaUpdateDto villaUpdateDto)
        //{
        //    if (id != villaUpdateDto.Id || villaUpdateDto == null)
        //    {
        //        return BadRequest();
        //    }
        //    Villa model = _mapper.Map<Villa>(villaUpdateDto);
        //    await _villasRepository.UpdateAsync(model);
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest();
        //    }
        //    return NoContent();
        //}

        [HttpPatch("{id:int}", Name = "UpdatePartialVillaAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<VillaDto>> UpdatePartialVillaAsync(int id, JsonPatchDocument<Villa> patchDto )
        {
            if(patchDto == null || id == 0)
            {
                return BadRequest();
            }
            var patchVilla = await _villasRepository.GetByIdAsync(c => c.Id == id, tracked: false);
            VillaUpdateDto villDto = _mapper.Map<VillaUpdateDto>(patchDto);
            if(patchVilla == null)
            {
                return BadRequest();
            }
            //patchDto.ApplyTo(villDto, ModelState);
            Villa model = _mapper.Map<Villa>(villDto);
            await _villasRepository.UpdateAsync(model);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return NoContent();
        }
    }
}
