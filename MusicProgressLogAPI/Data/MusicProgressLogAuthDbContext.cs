using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace MusicProgressLogAPI.Data
{
    public class MusicProgressLogAuthDbContext : IdentityDbContext
    {
        public MusicProgressLogAuthDbContext(DbContextOptions<MusicProgressLogAuthDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

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
            
            // Seed roles
            builder.Entity<IdentityRole>().HasData(roles);
        }
    }
}
