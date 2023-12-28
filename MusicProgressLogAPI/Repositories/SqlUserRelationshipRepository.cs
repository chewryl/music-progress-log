using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlUserRelationshipRepository : SqlRepositoryBase<UserRelationship>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlUserRelationshipRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
           _dbContext = dbContext;
        }

        public override async Task<UserRelationship?> GetByIdAsync(Guid id)
        {
            return await _dbContext.UserRelationships.Include(x => x.ProgressLogs).Include(x => x.Pieces).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
