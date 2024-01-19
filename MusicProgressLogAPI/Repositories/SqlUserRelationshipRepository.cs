﻿using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlUserRelationshipRepository : SqlRepositoryBase<ApplicationUser>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlUserRelationshipRepository(MusicProgressLogDbContext dbContext) 
            : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public override async Task<ApplicationUser?> GetByIdAsync(Guid id)
        {
            var identityUsers = await _dbContext.Users.FindAsync(id.ToString());
           // return await _dbContext.UserRelationships.Include(x => x.ProgressLogs).Include(x => x.Pieces).FirstOrDefaultAsync(x => x.Id == id);
            return null;
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
