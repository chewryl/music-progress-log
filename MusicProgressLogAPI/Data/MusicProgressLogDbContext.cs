using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Data
{
    public class MusicProgressLogDbContext : DbContext
    {
        public MusicProgressLogDbContext(DbContextOptions<MusicProgressLogDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<ProgressLog> ProgressLogs { get; set; }
        public DbSet<UserRelationship> UserRelationships { get; set; }
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<Piece> Pieces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRelationship>()
                .HasMany(x => x.ProgressLogs)
                .WithOne()
                .HasForeignKey(x => x.UserRelationshipId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<UserRelationship>()
                .HasMany(x => x.Pieces)
                .WithOne()
                .HasForeignKey(x => x.UserRelationshipId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgressLog>()
                .HasOne(x => x.AudioFile)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
