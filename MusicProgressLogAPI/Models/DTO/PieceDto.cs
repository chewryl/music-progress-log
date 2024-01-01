namespace MusicProgressLogAPI.Models.DTO
{
    public class PieceDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Composer { get; set; }
        public string Instrument { get; set; }
        public Guid UserRelationshipId { get; set; }
    }
}
