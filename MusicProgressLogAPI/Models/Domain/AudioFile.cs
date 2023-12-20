namespace MusicProgressLogAPI.Models.Domain
{
    public class AudioFile
    {
        public Guid Id { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileLocation { get; set; }
        public string FileName { get; set; }
        public string MIMEType { get; set; }
    }
}
