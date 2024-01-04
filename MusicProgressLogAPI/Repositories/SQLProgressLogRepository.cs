using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlProgressLogRepository : SqlRepositoryBase<ProgressLog>, IUserRelationshipRepository<ProgressLog>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlProgressLogRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<ProgressLog>> GetAllForUserAsync(Guid userRelationshipId)
        {
            return await _dbContext.ProgressLogs
                .Where(x => x.UserRelationshipId == userRelationshipId)
                .Include(x => x.Piece)
                .Include(x => x.AudioFile)
                .OrderBy(x => x.Date)
                .ToListAsync();
        }

        public override async Task<ProgressLog?> GetByIdAsync(Guid id)
        {
            return await _dbContext.ProgressLogs.Include(x => x.Piece).Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}