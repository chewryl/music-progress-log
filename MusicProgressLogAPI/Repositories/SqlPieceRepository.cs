using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlPieceRepository : SqlRepositoryBase<Piece>
    {
        private readonly MusicProgressLogDbContext _dbContext;
        public SqlPieceRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<Piece?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Pieces.Include(x => x.UserRelationship).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
