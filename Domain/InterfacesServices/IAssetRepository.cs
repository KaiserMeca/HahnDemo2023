﻿using Domain.Assets;

namespace Domain.InterfacesServices
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        Task<IEnumerable<Asset>> GetAllAsync();
        Task<Asset> GetForIdAsync(Guid id);
        Task<bool> AddAsync(Asset asset);
        Task<bool> UpdateAsync(Guid id, Asset asset);
        Task<bool> DeleteAsync(Guid id);

        Task<bool> countryExist(string country); //aggregate query
    }
}