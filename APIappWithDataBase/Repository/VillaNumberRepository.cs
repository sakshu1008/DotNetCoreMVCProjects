using APIappWithDataBase.Data;
using APIappWithDataBase.Models;
using APIappWithDataBase.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Linq.Expressions;

namespace APIappWithDataBase.Repository
{
    public class VillaNumberRepository : GenericRepository<VillaNumber>, IVillaNumberRepository
    {
        private readonly VillaDbContext _dbcontext;

        public VillaNumberRepository(VillaDbContext dbContext):base(dbContext)
        {
                _dbcontext = dbContext;
        }


        //public async Task CreateAsync(Villa villaEntity)
        //{
        //    await _dbcontext.Villas.AddAsync(villaEntity);
        //    await SaveAsync();
        //}

        //public async Task<List<Villa>> GetAllAsync(Expression<Func<Villa, bool>> filter = null)
        //{
        //    IQueryable<Villa> villaQueryable = _dbcontext.Villas;
        //    if(filter != null)
        //    {
        //        villaQueryable = villaQueryable.Where(filter);
        //    }
        //    return await villaQueryable.ToListAsync(); 
        //    //ToListAsync enables deferred execution
        //    //ToList causes immerdiate execution
        //}

        //public async Task<Villa> GetByIdAsync(Expression<Func<Villa, bool>> filter = null, bool tracked = true)
        //{
        //    IQueryable<Villa> villaQueryable = _dbcontext.Villas;
        //    if (!tracked)
        //    {
        //        villaQueryable = villaQueryable.AsNoTracking();
        //    }
        //    if (filter != null)
        //    {
        //        villaQueryable = villaQueryable.Where(filter);
        //    }
        //    return await villaQueryable.FirstOrDefaultAsync();
        //}

        //public async Task RemoveAsync(Villa villaEntity)
        //{
        //     _dbcontext.Villas.Remove(villaEntity);
        //    await SaveAsync();
        //}

        //public async Task SaveAsync()
        //{
        //    await _dbcontext.SaveChangesAsync();
        //}

        public async Task<VillaNumber> UpdateAsync(VillaNumber villaNumberEntity)
        {
            villaNumberEntity.UpdatedDate = DateTime.Now;
            _dbcontext.VillaNumbers.Update(villaNumberEntity);
            await _dbcontext.SaveChangesAsync(); 
            return villaNumberEntity;
        }
    }
}
