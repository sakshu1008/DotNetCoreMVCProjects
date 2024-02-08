using APIappWithDataBase.Models;
using APIappWithDataBase.Models.Dto;

namespace APIappWithDataBase.Repository.IRepository
{
    public interface IVillaRepository
    {
        Task<IEnumerable<VillaDto>> GetAll();
        Task<VillaDto> GetVillaByIdasync(int id);
        Task<int> AddVilla(VillaCreateDto villaCreateDto);
        Task<int> DeleteVilla(int id);
        Task<int> UpdateVilla(VillaUpdateDto villaUpdateDto);
    }
}
