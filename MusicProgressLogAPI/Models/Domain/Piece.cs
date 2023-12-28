using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MusicProgressLogAPI.Models.Domain
{
    public class Piece
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string Name { get; set; }

        [MaxLength(256)]
        public string Composer { get; set; }

        [MaxLength(256)]
        public string Instrument { get; set; }

        // Required foreign key
        public Guid UserRelationshipId { get; set; }
    }
}
