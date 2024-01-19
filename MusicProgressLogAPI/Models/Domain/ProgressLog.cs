using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicProgressLogAPI.Models.Domain
{
    public class ProgressLog
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Title { get; set; }

        public DateTime Date { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }

        // Required foreign keys
        public Guid PieceId { get; set; }
        public Guid UserId { get; set; }

        // Reference navigation to principal
        public Piece Piece { get; set; }

        // Reference navigation to dependent
        public AudioFile? AudioFile { get; set; }
    }
}
