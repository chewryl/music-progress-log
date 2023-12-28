using MusicProgressLogAPI.Data;
using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories
{
    public class SqlAudioFileRepository : SqlRepositoryBase<AudioFile>
    {
        public SqlAudioFileRepository(MusicProgressLogDbContext musicProgressLogDbContext) : base(musicProgressLogDbContext)
        {   
        }
    }
}
