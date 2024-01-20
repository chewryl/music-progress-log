using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Models.DTO
{
    public class UserResponseDto
    {
        public Guid Id { get; set; }

        public string UserName { get; set; }

        public ICollection<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();

        public ICollection<Piece> Pieces { get; set; } = new List<Piece>();
    }
}
