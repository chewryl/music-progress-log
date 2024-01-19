using Microsoft.AspNetCore.Identity;

namespace MusicProgressLogAPI.Models.Domain
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        // Collection navigation containing dependents
        public ICollection<ProgressLog> ProgressLogs { get; set; } = new List<ProgressLog>();

        public ICollection<Piece> Pieces { get; set; } = new List<Piece>();
    }
}