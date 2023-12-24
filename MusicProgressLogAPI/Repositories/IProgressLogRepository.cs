using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public interface IProgressLogRepository
    {
        Task<IEnumerable<ProgressLog>> GetAllAsync();

        Task<ProgressLog?> GetByIdAsync(Guid id);

        Task<ProgressLog> CreateAsync(ProgressLog progressLog);

        Task<ProgressLog?> UpdateAsync(Guid id, ProgressLog progressLog);

        Task<Guid?> DeleteAsync(Guid id);
    }
}
