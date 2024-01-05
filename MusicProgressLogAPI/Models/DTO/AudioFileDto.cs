using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.DTO
{
    public class AudioFileDto
    {
        public Guid Id { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileLocation { get; set; }
        [Required]
        public string FileName { get; set; }
        [Required]
        public string MIMEType { get; set; }
    }
}
