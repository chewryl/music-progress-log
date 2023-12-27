using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Models.Domain;
using System.Reflection.Metadata;

namespace MusicProgressLogAPI.Data
{
    public class MusicProgressLogDbContext : DbContext
    {
        public MusicProgressLogDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<ProgressLog> ProgressLogs { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<Piece> Pieces { get; set; }
    }
}
