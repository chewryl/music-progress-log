﻿using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Repositories.Interfaces;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlPieceRepository : SqlRepositoryBase<Piece>, IUserRepository<Piece>
    {
        private readonly MusicProgressLogDbContext _dbContext;

        public SqlPieceRepository(MusicProgressLogDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<Piece>> GetAllForUserWithFilterAsync(Guid userId, string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 30)
        {
            var pieces = _dbContext.Pieces
                .Where(x => x.UserId == userId)
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (!string.IsNullOrEmpty(filterOn) && !string.IsNullOrEmpty(filterQuery))
            {
                if (filterOn.Equals("Piece", StringComparison.OrdinalIgnoreCase))
                {
                    pieces = pieces.Where(x => x.Name.Contains(filterQuery));
                }
            }

            var skipResults = (pageNumber - 1) * pageSize;

            return await pieces.Skip(skipResults).Take(pageSize).ToListAsync();
        }
    }
}
