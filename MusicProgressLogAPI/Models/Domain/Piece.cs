namespace MusicProgressLogAPI.Models.Domain
{
    public class Piece
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public string Instrument { get; set; }

        // Required foreign key
        public Guid UserRelationshipId { get; set; }

        // Required reference navigation to parent
        public UserRelationship UserRelationship { get; set; } = null!;
    }
}
