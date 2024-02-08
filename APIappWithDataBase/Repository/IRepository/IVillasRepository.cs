using APIappWithDataBase.Models;
using System.Linq.Expressions;

namespace APIappWithDataBase.Repository.IRepository
{
    public interface IVillasRepository:IGenericRepository<Villa>
    {
        //Task<List<Villa>> GetAllAsync(Expression<Func<Villa,bool>> filter=null);

        //Task<Villa> GetByIdAsync(Expression<Func<Villa,bool>> filter = null,bool tracked = true);
        //Task CreateAsync(Villa villaEntity);
        Task<Villa> UpdateAsync(Villa villaEntity);
        //Task RemoveAsync(Villa villaEntity);
        //Task SaveAsync();
    }
}
