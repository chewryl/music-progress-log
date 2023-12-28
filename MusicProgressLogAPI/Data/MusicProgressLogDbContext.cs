using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using MusicProgressLogAPI.Models.Domain;

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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRelationship>()
                .HasMany(e => e.Pieces)
                .WithOne(p => p.UserRelationship)
                .HasForeignKey(e => e.UserRelationshipId)
                .HasPrincipalKey(e => e.Id)
                .OnDelete(DeleteBehavior.NoAction);
            modelBuilder.Entity<UserRelationship>()
                .HasMany(e => e.ProgressLogs)
                .WithOne(p => p.UserRelationship)
                .HasPrincipalKey(e => e.Id)
                .HasForeignKey(p => p.UserRelationshipId)
                .OnDelete(DeleteBehavior.NoAction);
            //modelBuilder.Entity<Piece>()
            //    .HasOne(p => p.UserRelationship)
            //    .WithMany(u => u.Pieces)
            //    .HasForeignKey(p => p.UserRelationshipId)
            //    .OnDelete(DeleteBehavior.NoAction);
                
        }
    }
}
