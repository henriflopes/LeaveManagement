namespace LeaveManagement.Application.Contracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetAsync(int? id);
        Task<T> AddAsync(T entity);
        Task AddRangeAsync(List<T> entities);
        Task<List<T>> GetAllAsync();
        Task<int> GetCountAsync();
        Task<bool> Exists(int id);
        Task DeleteAsync(int id);
        Task UpdateAsync(T entity);

    }
}