namespace MayStats_Infra.Interfaces.Repositories.Base
{
    public interface IEFRepository<TId, TTable> where TTable : class
    {
        Task<IEnumerable<TTable>> GetAllAsync();
        Task<TTable> GetByIdAsync(TId id);
        Task AddAsync(TTable entity);
        Task UpdateAsync(TTable entity);
        Task DeleteAsync(TId id);
    }
}
