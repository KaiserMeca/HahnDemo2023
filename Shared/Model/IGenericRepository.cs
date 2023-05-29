using System.Linq.Expressions;

namespace Shared.Model
{
    /// <summary>
    /// Represents an interface for a generic repository.
    /// </summary>
    /// <typeparam name="T">The entity type of the repository.</typeparam>
    public interface IGenericRepository<T> where T : AgregateRoot
    {
        /// <summary>
        /// Gets all entities asynchronously.
        /// </summary>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Gets an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        Task<T> GetForIdAsync(Guid id);

        /// <summary>
        /// Adds an entity asynchronously.
        /// </summary>
        /// <param name="entity">The entity to add.</param>
        Task<bool> AddAsync(T entity);

        /// <summary>
        /// Updates an entity asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to update.</param>
        /// <param name="entity">The updated entity.</param>
        Task<bool> UpdateAsync(Guid id, T entity);

        /// <summary>
        /// Deletes an entity by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the entity to delete.</param>
        Task<bool> DeleteAsync(Guid id);
    }
}
