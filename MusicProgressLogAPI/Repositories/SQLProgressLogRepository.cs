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

        public async Task<IEnumerable<ProgressLog>> GetAllForUserWithFilterAsync(Guid userRelationshipId, string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 30)
        {
            var progressLogs = _dbContext.ProgressLogs
                .Where(x => x.UserId == userRelationshipId)
                .Include(x => x.Piece)
                .Include(x => x.AudioFile)
                .OrderBy(x => x.Date)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("piece", StringComparison.OrdinalIgnoreCase))
                {
                    progressLogs = progressLogs.Where(x => x.Piece.Name.Contains(filterQuery));
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await progressLogs.Skip(skipResults).Take(pageSize).ToListAsync();

        }

        public override async Task<ProgressLog?> GetByIdAsync(Guid id)
        {
            return await _dbContext.ProgressLogs.Include(x => x.Piece).Include(x => x.AudioFile).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}