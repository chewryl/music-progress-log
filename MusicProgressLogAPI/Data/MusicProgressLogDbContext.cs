using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Models.Domain;
using System.Security.Cryptography.Xml;

namespace MusicProgressLogAPI.Data
{
    public class MusicProgressLogDbContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public MusicProgressLogDbContext(DbContextOptions<MusicProgressLogDbContext> dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<ProgressLog> ProgressLogs { get; set; }
        public DbSet<AudioFile> AudioFiles { get; set; }
        public DbSet<Piece> Pieces { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var readerRoleId = "7e282516-d7c5-44b6-a926-36c92fc45d11";
            var writerRoleId = "c052a91e-6547-46e6-98f6-59826a751d62";

            var roles = new List<IdentityRole>
            {
                new IdentityRole
                {
                    Id = readerRoleId,
                    ConcurrencyStamp = readerRoleId,
                    Name = "Reader",
                    NormalizedName = "Reader".ToUpper()
                },
                new IdentityRole
                {
                    Id = writerRoleId,
                    ConcurrencyStamp = writerRoleId,
                    Name = "Writer",
                    NormalizedName = "Writer".ToUpper()
                }
            };

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.ProgressLogs)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(x => x.Pieces)
                .WithOne()
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgressLog>()
                .HasOne(x => x.AudioFile)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<ProgressLog>()
                .HasOne(x => x.Piece)
                .WithMany()
                .OnDelete(DeleteBehavior.NoAction);

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
