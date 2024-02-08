using APIappWithDataBase.Models;
using System.Linq.Expressions;

namespace APIappWithDataBase.Repository.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter = null);

        Task<T> GetByIdAsync(Expression<Func<T, bool>> filter = null, bool tracked = true);
        Task CreateAsync(T villaEntity);
        Task RemoveAsync(T villaEntity);
        Task SaveAsync();
    }
}
