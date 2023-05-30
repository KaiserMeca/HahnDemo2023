using Shared.Model;

namespace Domain.InterfacesServices
{
    /// <summary>
    /// Represents the interface for a Unit of Work.
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Saves changes asynchronously for the specified aggregate root.
        /// </summary>
        /// <param name="aggregateRoot">Added root to save changes access event provider</param>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveAsync(AgregateRoot aggregateRoot);

        /// <summary>
        /// Saves all changes asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation.</returns>
        Task SaveAsync();

        /// <summary>
        /// Performs tasks associated with releasing resources.
        /// </summary>
        void Dispose();
    }
}
