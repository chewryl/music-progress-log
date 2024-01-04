﻿namespace MusicProgressLogAPI.Models.DTO
{
    public class AudioFileDto
    {
        public Guid Id { get; set; }
        public byte[]? FileData { get; set; }
        public string? FileLocation { get; set; }
        public string FileName { get; set; }
        public string MIMEType { get; set; }
    }
}
