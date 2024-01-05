using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.DTO
{
    public class PieceDto
    {
        public Guid Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Composer { get; set; }
        [Required]
        public string Instrument { get; set; }
        public Guid UserRelationshipId { get; set; }
    }
}
