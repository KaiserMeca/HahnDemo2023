using Domain.Assets;

namespace Domain.InterfacesServices
{
    public interface IAssetService
    {
        Task<IEnumerable<AssetDTO>> GetAllAsync();
        Task<AssetDTO> GetForIdAsync(Guid id);
        Task<bool> AddAsync(AssetDTO asset);
        Task<bool> UpdateAsync(Guid id, AssetDTO asset);
        Task<bool> DeleteAsync(Guid id);
        Task<bool> countryExist(string country); 
    }
}
