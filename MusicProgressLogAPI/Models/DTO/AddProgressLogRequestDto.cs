using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Models.DTO
{
    public class AddProgressLogRequestDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public AudioFile AudioFile { get; set; }
    }
}
