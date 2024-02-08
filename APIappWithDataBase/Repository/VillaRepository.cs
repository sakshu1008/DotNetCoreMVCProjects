using APIappWithDataBase.Data;
using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;
using APIappWithDataBase.Repository.IRepository;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace APIappWithDataBase.Repository
{
    public class VillaRepository : IVillaRepository
    {
        private readonly IMapper _mapper;
        private readonly VillaDbContext _dbContext;

        public VillaRepository(VillaDbContext dbContext,IMapper mapper)
        {
                _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<int> AddVilla(VillaCreateDto villaCreateDto)
        {
            if (villaCreateDto == null)
            {
                throw new Exception("Add Villa Page Not found!!!Please try again");
            }
            Villa villaToAdd = _mapper.Map<Villa>(villaCreateDto);
            await _dbContext.Villas.AddAsync(villaToAdd);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<int> DeleteVilla(int id)
        {
            if(id == null)
            {
                throw new Exception("Delete Villa Page Not found!!!Please try again");
            }
            var villaById = await _dbContext.Villas.FindAsync(id);
            Villa villaToDelete = _mapper.Map<Villa>(villaById);
            if(villaToDelete == null)
            {
                throw new Exception("Villa data to Delete Not found!!!Please try again");
            }
            _dbContext.Villas.Remove(villaToDelete);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<VillaDto>> GetAll()
        {
            IEnumerable<Villa> VillaList = await _dbContext.Villas.ToListAsync();
            if(VillaList == null)
            {
                throw new Exception("Villa List Not Found!!!,Please add new one");
            }
            return _mapper.Map<IEnumerable<VillaDto>>(VillaList);
        }

        public async Task<VillaDto> GetVillaByIdasync(int id)
        {
            if(id == null)
            {
                throw new Exception("Get Villa by Id Page Not found!!!Please try again");
            }
            var villaById = await _dbContext.Villas.FindAsync(id);
            return _mapper.Map<VillaDto>(villaById);
        }

        public async Task<int> UpdateVilla(VillaUpdateDto villaUpdateDto)
        {
            var villaToUpdate = await _dbContext.Villas.FirstOrDefaultAsync(x => x.Id == villaUpdateDto.Id);
            if(villaToUpdate != null)
            {
                villaToUpdate.Name = villaUpdateDto.Name;
                villaToUpdate.Details = villaUpdateDto.Details;
                villaToUpdate.Rate = villaUpdateDto.Rate;
                villaToUpdate.Sqft = villaUpdateDto.Sqft;
                villaToUpdate.Occupancy = villaUpdateDto.Occupancy;
                villaToUpdate.ImageUrl = villaUpdateDto.ImageUrl;
                villaToUpdate.Amenity = villaUpdateDto.Amenity;

                var updatedVilla = _mapper.Map<Villa>(villaToUpdate);
                _dbContext.Villas.Update(updatedVilla);
                return await _dbContext.SaveChangesAsync();
            }
            else
            {
                throw new Exception($"Villa to update is not found!!!");
            }
        }
    }
}
