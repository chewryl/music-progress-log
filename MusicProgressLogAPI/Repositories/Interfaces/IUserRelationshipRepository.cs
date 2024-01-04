using MusicProgressLogAPI.Models.Domain;

namespace MusicProgressLogAPI.Repositories.Interfaces
{
    public interface IUserRelationshipRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllForUserAsync(Guid userRelationshipId);
    }
}
