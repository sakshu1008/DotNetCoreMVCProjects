using APIappWithDataBase.Data;
using APIappWithDataBase.Models;
using APIappWithDataBase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace APIappWithDataBase.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly VillaDbContext _dbContext;
        internal DbSet<T> dbset;

        public GenericRepository(VillaDbContext dbContext)
        {
            _dbContext = dbContext;
            this.dbset = _dbContext.Set<T>();
        }
        public async Task CreateAsync(T villaEntity)
        {
            await dbset.AddAsync(villaEntity);
            await SaveAsync();
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null)
        {
            IQueryable<T> villasQuerable = dbset;
            if (filter != null)
            {
                villasQuerable = villasQuerable.Where(filter);
            }
            return await villasQuerable.ToListAsync();
        }

        public async Task<T> GetByIdAsync(Expression<Func<T, bool>>? filter = null, bool tracked = true)
        {
            IQueryable<T> villasQuery = dbset;
            if(!tracked)
            {
                villasQuery = villasQuery.AsNoTracking();
            }
            if(filter != null)
            {
                villasQuery = villasQuery.Where(filter);
            }
            return await villasQuery.FirstOrDefaultAsync();
        }

        public async Task RemoveAsync(T villaEntity)
        {
            dbset.Remove(villaEntity);
            await SaveAsync();
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
