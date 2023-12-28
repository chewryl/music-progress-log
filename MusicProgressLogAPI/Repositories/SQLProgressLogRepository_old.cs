using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SQLProgressLogRepository_old : IProgressLogRepository
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SQLProgressLogRepository_old(MusicProgressLogDbContext dbContext)
        {
                _dbContext = dbContext;
        }

        public async Task<ProgressLog?> GetByIdAsync(Guid id)
        {
            try
            {
                return await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<IEnumerable<ProgressLog>> GetAllAsync()
        {
            try
            {
                return await _dbContext.ProgressLogs.Include(x => x.AudioFile).ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ProgressLog> CreateAsync(ProgressLog progressLog)
        {
            try
            {
                await _dbContext.ProgressLogs.AddAsync(progressLog);
                await _dbContext.SaveChangesAsync();
                return progressLog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<ProgressLog?> UpdateAsync(Guid id, ProgressLog progressLog)
        {
            try
            {
                var existingProgressLog = await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);

                if (existingProgressLog == null)
                {
                    return null;
                }

                existingProgressLog.Title = progressLog.Title;
                existingProgressLog.Description = progressLog.Description;
                existingProgressLog.AudioFile.FileName = progressLog.AudioFile.FileName;
                existingProgressLog.AudioFile.FileData = progressLog.AudioFile.FileData;
                existingProgressLog.AudioFile.FileLocation = progressLog.AudioFile.FileLocation;
                existingProgressLog.AudioFile.MIMEType = progressLog.AudioFile.MIMEType;

                await _dbContext.SaveChangesAsync();
                return existingProgressLog;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }

        public async Task<Guid?> DeleteAsync(Guid id)
        {
            try
            {
                var progressLog = await _dbContext.ProgressLogs.Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);

                if (progressLog == null)
                {
                    return null;
                }

                _dbContext.ProgressLogs.Remove(progressLog);
                _dbContext.AudioFiles.Remove(progressLog.AudioFile);
                
                await _dbContext.SaveChangesAsync();
                return progressLog.Id;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
        }
    }
}
