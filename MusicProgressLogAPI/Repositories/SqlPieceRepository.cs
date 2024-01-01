using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlPieceRepository : SqlRepositoryBase<Piece>
    {
        public SqlPieceRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
