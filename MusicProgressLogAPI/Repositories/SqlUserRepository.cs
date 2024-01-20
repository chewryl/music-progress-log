using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlUserRepository : SqlRepositoryBase<ApplicationUser>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlUserRepository(MusicProgressLogDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            return await _dbContext.Users.Include(x => x.ProgressLogs).Include(x => x.Pieces).FirstOrDefaultAsync(x => x.Id == id);
        }

        public override async Task<Guid?> DeleteAsync(Guid id)
        {
            var user = await GetByIdAsync(id);
            if (user == null)
            {
                return null;
            }

            _dbContext.Attach(user);
            _dbContext.Entry(user).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();
            return id;
        }
    }
}
