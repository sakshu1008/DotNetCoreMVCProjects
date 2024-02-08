using APIappWithDataBase.Models;

namespace APIappWithDataBase.Repository.IRepository
{
    public interface IVillaNumberRepository: IGenericRepository<VillaNumber>
    {
        Task<VillaNumber> UpdateAsync(VillaNumber villaNumber);
    }
}
