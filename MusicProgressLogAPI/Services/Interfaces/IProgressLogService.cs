using MusicProgressLogAPI.Models;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Services.Interfaces
{
    public interface IProgressLogService
    {
        Task<ProgressLogConfig> GetAllProgressLogsForUser(Guid userId, string? filterOn = null, string? filterQuery = null, int pageNumer = 1, int pageSize = 30);
        Task<ProgressLogConfig> GetProgressLogForUser(Guid userId, Guid progressLogId);
        Task<ProgressLogConfig> AddProgressLogForUser(Guid userId, ProgressLog progressLog);
        Task<ProgressLogConfig> UpdateProgressLog(Guid progressLogId, ProgressLog progressLog);
        Task<ProgressLogConfig> DeleteProgressLog(Guid progressLogId);
    }
}
