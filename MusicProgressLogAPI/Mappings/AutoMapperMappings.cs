using AutoMapper;
using MusicProgressLogAPI.Models.Domain;
using MusicProgressLogAPI.Models.DTO;

namespace MusicProgressLogAPI.Mappings
{
    public class AutoMapperMappings : Profile
    {
        public AutoMapperMappings()
        {
            CreateMap<ProgressLogDto, ProgressLog>().ReverseMap();
            CreateMap<AudioFileDto, AudioFile>().ReverseMap();
            CreateMap<PieceDto, Piece>().ReverseMap();
            CreateMap<UserRelationshipDto, ApplicationUser>().ReverseMap();
        }
    }
}
