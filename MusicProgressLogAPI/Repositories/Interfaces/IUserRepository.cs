namespace MusicProgressLogAPI.Repositories.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllForUserWithFilterAsync(Guid userId, string? filterOn = null, string? filterQuery = null, int pageNumber = 1, int pageSize = 30);
    }
}
