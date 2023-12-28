using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlProgressLogRepository : SqlRepositoryBase<ProgressLog>
    {
        public SqlProgressLogRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
        }
    }
}
