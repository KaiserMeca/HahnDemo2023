using Domain.Security;

namespace Domain.Repositoy
{
    public interface IAssetRepository : IGenericRepository<AssetDTO>
    {
        Task<IEnumerable<AssetDTO>> GetAllAsync();
        Task<AssetDTO> GetForIdAsync(Guid id);
        Task<bool> AddAsync(AssetDTO asset);
        Task<bool> UpdateAsync(Guid id, AssetDTO asset);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> countryExist(string country); //aggregate query
    }
}
