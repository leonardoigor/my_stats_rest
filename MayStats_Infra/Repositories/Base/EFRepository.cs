using MayStats_Infra.Interfaces.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace MayStats_Infra.Repositories.Base
{


    public class EFRepository<TId, TTable> : IEFRepository<TId, TTable> where TTable : class
    {
        private readonly DbContext _context;
        private readonly DbSet<TTable> _dbSet;

        public EFRepository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TTable>();
        }

        public async Task<IEnumerable<TTable>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TTable> GetByIdAsync(TId id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task AddAsync(TTable entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(TTable entity)
        {
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(TId id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
