using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;
using APIappWithDataBase.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace APIappWithDataBase.Controllers
{
    [Route("api/villanumber")]
    [ApiController]
    public class VillaNumberController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVillaNumberRepository _villaNumberRepository;
        private readonly IVillaRepository _villaRepository;
        protected APIResponse _apiResponse;

        public VillaNumberController(IMapper mapper, IVillaNumberRepository villaNumberRepository, IVillaRepository villaRepository)
        {
            _mapper = mapper;
            _villaNumberRepository = villaNumberRepository;
            this._apiResponse = new APIResponse();
            _villaRepository = villaRepository;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetVillaNumbersAsync()
        {
            try
            {
                IEnumerable<VillaNumber> villaNumberList = await _villaNumberRepository.GetAllAsync();
                _apiResponse.Result = _mapper.Map<VillaNumberDto>(villaNumberList);
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
        
        [HttpGet("{id:int}")]
        public async Task<ActionResult<APIResponse>> GetVillaNumberAsync(int id)
        {
            if (id == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.BadRequest;
                return BadRequest(_apiResponse);
            }
            var villaNumberById = await _villaNumberRepository.GetByIdAsync(v => v.VillaNo == id);
            if (villaNumberById == null)
            {
                _apiResponse.StatusCode = HttpStatusCode.NotFound;
                return NotFound(_apiResponse);
            }
            _apiResponse.Result = _mapper.Map<VillaNumberDto>(villaNumberById);
            _apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> AddVilaaAsync([FromBody] VillaNumberCreateDto villaNumberCreateDto)
        {
            var villasList = await _villaNumberRepository.GetAllAsync();
            var villaNumberExists = villasList.FirstOrDefault(x => x.VillaNo == villaNumberCreateDto.VillaNo);
            if (villaNumberExists != null)
            {
                ModelState.AddModelError("CustomErrorMsg", $"Villa Number already exists!");
                return BadRequest();
            }
            var villaList = await _villaRepository.GetAll();
            var villaExists = villaList.FirstOrDefault(x => x.Id == villaNumberCreateDto.VillaId);
            if (villaExists != null)
            {
                ModelState.AddModelError("CustomErrorMsg", $"Villa Number already exists!");
                return BadRequest();
            }
            if (villaNumberCreateDto == null)
            {
                return BadRequest(villaNumberCreateDto);
            }
            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberCreateDto);
            await _villaNumberRepository.CreateAsync(model);
            _apiResponse.Result = _mapper.Map<VillaNumberDto>(model);
            _apiResponse.StatusCode = HttpStatusCode.OK;
            return Ok(_apiResponse);
        }

        [HttpDelete("{villano:int}", Name = "DeleteVillaNumberAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> DeleteVillaNumberAsync(int villano)
        {
            if (villano == 0)
            {
                return BadRequest();
            }
            var villaNumberDelete = await _villaNumberRepository.GetByIdAsync(v => v.VillaNo == villano);
            if (villaNumberDelete == null)
            {
                return NotFound();
            }
            await _villaNumberRepository.RemoveAsync(villaNumberDelete);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsSuccess = true;
            return Ok(_apiResponse);
        }

        [HttpPut("{villano:int}", Name = "UpdateVillaNumberAsync")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateVillaNumberAsync(int villano, [FromBody] VillaNumberUpdateDto villaNumberUpdateDto)
        {
            if (villano != villaNumberUpdateDto.VillaNo || villaNumberUpdateDto == null)
            {
                return BadRequest();
            }
            var villasList = await _villaRepository.GetAll();
            var villaNumberExists = villasList.FirstOrDefault(x => x.Id == villaNumberUpdateDto.VillaId);
            if (villaNumberExists != null)
            {
                ModelState.AddModelError("CustomErrorMsg", $"Villa Number already exists!");
                return BadRequest();
            }
            VillaNumber model = _mapper.Map<VillaNumber>(villaNumberUpdateDto);
            await _villaNumberRepository.UpdateAsync(model);
            _apiResponse.StatusCode = HttpStatusCode.NoContent;
            _apiResponse.IsSuccess = true;
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            return Ok(_apiResponse);
        }
    }
}
