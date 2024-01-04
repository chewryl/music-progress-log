using MusicProgressLogAPI.Models.Domain;
using Newtonsoft.Json;

namespace MusicProgressLogAPI.Models.DTO
{
    public class ProgressLogDto
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public string Title { get; set; }
        
        [JsonProperty]
        public DateTime Date { get; set; }
        
        [JsonProperty]
        public string? Description { get; set; }

        [JsonProperty]
        public Guid PieceId { get; set; }

        [JsonProperty("audioFile")]
        public AudioFileDto? AudioFile { get; set; }
    }
}
