using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.Domain
{
    public class AudioFile
    {
        public Guid Id { get; set; }

        public byte[]? FileData { get; set; }

        [MaxLength(256)]
        public string? FileLocation { get; set; }

        [MaxLength(256)]
        public string FileName { get; set; }

        [MaxLength(256)]
        public string MIMEType { get; set; }

        // Required foreign keys
        public Guid ProgressLogId { get; set; }
    }
}
