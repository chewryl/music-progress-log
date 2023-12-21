namespace MusicProgressLogAPI.Models.Domain
{
    public class ProgressLog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public AudioFile AudioFile { get; set; }
    }
}
