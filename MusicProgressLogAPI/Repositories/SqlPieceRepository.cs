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
    }
}
