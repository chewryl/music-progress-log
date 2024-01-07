using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlPieceRepository : SqlRepositoryBase<Piece>, IUserRelationshipRepository<Piece>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlPieceRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Piece>> GetAllForUserWithFilterAsync(Guid userRelationshipId, string? filterOn = null, string? filterQuery = null)
        {
            var pieces = _dbContext.Pieces
                .Where(x => x.UserRelationshipId == userRelationshipId)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Name", StringComparison.OrdinalIgnoreCase))
                {
                    pieces = pieces.Where(x => x.Name.Contains(filterQuery));
                }
            }

            return await pieces.ToListAsync();
        }
    }
}
