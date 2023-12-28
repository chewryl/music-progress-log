namespace MusicProgressLogAPI.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> GetByIdAsync(Guid id);
        Task<T> CreateAsync(T obj);
        void UpdateAsync(Guid id, T obj);
        Task<Guid?> DeleteAsync(Guid id);
        Task SaveAsync();
    }
}
