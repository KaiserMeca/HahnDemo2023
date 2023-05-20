using Common.Model;
using Domain.Security;

namespace Domain.Repositoy
{
    public interface IGenericRepository<T> where T : AgregateRoot
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetForIdAsync(Guid id);
        Task<bool> AddAsync(T asset);
        Task<bool> UpdateAsync(Guid id, T asset);
        Task<bool> DeleteAsync(Guid id);
    }
}