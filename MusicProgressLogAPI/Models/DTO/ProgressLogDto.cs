using MusicProgressLogAPI.Models.Domain;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.DTO
{
    public class ProgressLogDto
    {
        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        [Required]
        public string Title { get; set; }
        
        [JsonProperty]
        public DateTime Date { get; set; }
        
        [JsonProperty]
        public string? Description { get; set; }

        [JsonProperty]
        [Required]
        public Guid PieceId { get; set; }

        [JsonProperty("audioFile")]
        public AudioFileDto? AudioFile { get; set; }

        public Guid UserId { get; set; }
    }
}
