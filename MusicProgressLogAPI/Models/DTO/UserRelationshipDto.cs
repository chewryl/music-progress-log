﻿using System.ComponentModel.DataAnnotations;

namespace MusicProgressLogAPI.Models.DTO
{
    public class UserRelationshipDto
    {
        public Guid Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
