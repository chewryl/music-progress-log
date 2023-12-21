using Microsoft.EntityFrameworkCore;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Data
{
    public class MusicProgressLogDbContext : DbContext
    {
        public MusicProgressLogDbContext(DbContextOptions dbContextOptions)
            :base(dbContextOptions)
        {
        }

        public DbSet<AudioFile> AudioFiles { get; set; }

        public DbSet<ProgressLog> ProgressLogs { get; set; }
    }
}
