using Domain.Security;

namespace Domain.Repositoy
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset> GetForIdAsync(Guid id);
        Task<bool> AddAsync(AssetDTO asset);
        Task<bool> UpdateAsync(Guid id, Asset asset);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> countryExist(string country); //aggregate query
    }
}
