using Domain.Assets.Models;
using Shared.Model;

namespace Domain.InterfacesServices
{
    /// <summary>
    /// Represents the interface for the asset repository
    /// </summary>
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        /// <summary>
        /// Retrieves all assets asynchronously
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and contains the collection of assets</returns>
        Task<IEnumerable<Asset>> GetAllAsync();

        /// <summary>
        /// Retrieves an asset by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the asset to retrieve</param>
        /// <returns>A task that represents the asynchronous operation and contains the retrieved asset</returns>
        Task<Asset> GetForIdAsync(Guid id);

        /// <summary>
        /// Adds a new asset asynchronously
        /// </summary>
        /// <param name="asset">The asset to add</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was added successfully, or false otherwise</returns>
        Task<bool> AddAsync(Asset asset);

        /// <summary>
        /// Updates an asset asynchronously.
        /// </summary>
        /// <param name="id">The ID of the asset to update</param>
        /// <param name="asset">The updated asset</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was updated successfully, or false otherwise</returns>
        Task<bool> UpdateAsync(Guid id, Asset asset);

        /// <summary>
        /// Deletes an asset asynchronously.
        /// </summary>
        /// <param name="id">The ID of the asset to delete</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was deleted successfully, or false otherwise</returns>
        Task<bool> DeleteAsync(Guid id);
    }
}
