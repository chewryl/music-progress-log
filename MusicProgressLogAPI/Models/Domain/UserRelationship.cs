using System.Collections.ObjectModel;

namespace MusicProgressLogAPI.Models.Domain
{
    public class UserRelationship
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }

        // Collection navigation containing dependents
        public ICollection<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();

        public ICollection<Piece> Pieces { get; set; } = new List<Piece>();
    }
}