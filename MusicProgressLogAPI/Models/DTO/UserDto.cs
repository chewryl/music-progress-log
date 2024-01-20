using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }
    }
}
