using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MusicProgressLogAPI.Models.DTO
{
    public class UserRequestDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
