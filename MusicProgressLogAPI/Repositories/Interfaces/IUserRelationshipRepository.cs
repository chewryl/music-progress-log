namespace MusicProgressLogAPI.Repositories.Interfaces
{
    public interface IUserRelationshipRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllForUserWithFilterAsync(Guid userRelationshipId, string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 30);
    }
}
