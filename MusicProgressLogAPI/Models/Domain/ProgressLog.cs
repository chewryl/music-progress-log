namespace MusicProgressLogAPI.Models.Domain
{
    public class ProgressLog
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }

        // Required foreign keys
        public Guid PieceId { get; set; }
        public Guid UserRelationshipId { get; set; }

        // Optional foreign keys
        public Guid? AudioFileId { get; set; }

        // Reference navigation to dependendents
        public Piece Piece { get; set; }
        public UserRelationship UserRelationship { get; set; } = null!;
        public AudioFile? AudioFile { get; set; }

    }
}
