using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories.Interfaces
{
    public interface ITokenRepository
    {
        string CreateJwt(ApplicationUser user, List<string> roles);
    }
}
