using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Models.DTO
{
    public class UpdateProgressLogRequestDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public AudioFileDto AudioFile { get; set; }
    }
}
