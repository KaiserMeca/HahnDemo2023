using Domain.Assets.Models;

namespace Domain.InterfacesServices
{
    /// <summary>
    /// Represents the interface for the asset service
    /// </summary>
    public interface IAssetService
    {
        /// <summary>
        /// Retrieves all assets asynchronously
        /// </summary>
        /// <returns>A task that represents the asynchronous operation and contains the collection of asset DTOs</returns>
        Task<IEnumerable<AssetDTO>> GetAllAsync();

        /// <summary>
        /// Retrieves an asset by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the asset to retrieve</param>
        /// <returns>A task that represents the asynchronous operation and contains the retrieved asset DTO</returns>
        Task<AssetDTO> GetForIdAsync(Guid id);

        /// <summary>
        /// Adds a new asset asynchronously
        /// </summary>
        /// <param name="asset">The asset DTO to add</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was added successfully, or false otherwise</returns>
        Task<bool> AddAsync(AssetDTO asset);

        /// <summary>
        /// Updates an asset asynchronously.
        /// </summary>
        /// <param name="id">The ID of the asset to update</param>
        /// <param name="asset">The updated asset DTO</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was updated successfully, or false otherwise</returns>
        Task<bool> UpdateAsync(Guid id, AssetDTO asset);

        /// <summary>
        /// Deletes an asset asynchronously.
        /// </summary>
        /// <param name="id">The ID of the asset to delete</param>
        /// <returns>A task that represents the asynchronous operation and returns true if the asset was deleted successfully, or false otherwise</returns>
        Task<bool> DeleteAsync(Guid id);

        Task<bool> SendMail(string mail);
    }
}
