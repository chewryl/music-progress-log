using MusicProgressLogAPI.Models;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Services.Interfaces
{
    public interface IProgressLogService
    {
        Task<ProgressLogConfig> GetAllProgressLogsForUser(Guid userRelationshipId, string? filterOn = null, string? filterQuery = null);
        Task<ProgressLogConfig> GetProgressLogForUser(Guid userRelationshipId, Guid progressLogId);
        Task<ProgressLogConfig> AddProgressLogForUser(Guid userRelationshipId, ProgressLog progressLog);
        Task<ProgressLogConfig> UpdateProgressLog(Guid progressLogId, ProgressLog progressLog);
        Task<ProgressLogConfig> DeleteProgressLog(Guid progressLogId);
    }
}
