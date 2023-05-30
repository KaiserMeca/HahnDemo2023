using Domain.InterfacesServices;
using Domain.Assets.Aggregates.Events;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using Domain.Assets.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    /// <summary>
    /// Asset repository implementation
    /// </summary>
    public class AssetRepository : IAssetRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AssetContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssetRepository"/> class
        /// </summary>
        /// <param name="context">The asset context</param>
        /// <param name="unitOfWork">The unit of work</param>
        public AssetRepository(AssetContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds an asset asynchronously
        /// </summary>
        /// <param name="asset">The asset to add</param>
        /// <returns>A boolean indicating whether the asset was added successfully</returns>
        public async Task<bool> AddAsync(Asset asset)
        {
            if (await _context.Assets.AnyAsync(x => x.Id == asset.Id))
            {
                throw new RepeatedIdException("The user code already exists");
            }
            if (asset == null)
            {
                return false;
            }
            else
            {
                await _context.Assets.AddAsync(asset);
                await _unitOfWork.SaveAsync(asset);
                return true;
            }
        }

        /// <summary>
        /// Retrieves all assets asynchronously
        /// </summary>
        /// <returns>A collection of assets</returns>
        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            var assets = await _context.Assets.ToListAsync();
            return assets ?? new List<Asset>();
        }

        /// <summary>
        /// Retrieves an asset by its ID asynchronously
        /// </summary>
        /// <param name="id">The ID of the asset to retrieve</param>
        /// <returns>The asset with the specified ID, or null if not found</returns>
        public async Task<Asset> GetForIdAsync(Guid id)
        {
            Asset asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);
            if (asset != null)
            {
                return asset;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Updates an asset asynchronously
        /// </summary>
        /// <param name="id">The ID of the asset to update</param>
        /// <param name="asset">The updated asset dat.</param>
        /// <returns>A boolean indicating whether the asset was updated successfully</returns>
        public async Task<bool> UpdateAsync(Guid id, Asset asset)
        {
            Asset? assetDB = await _context.Assets.FindAsync(asset.Id);
            if (assetDB == null)
            {
                return false;
            }

            assetDB.ApplyUpdateAssetData(new UpdateAssetData(asset.Name, asset.DepartmentMail, asset.Department, asset.PurchaseDate, asset.Lifespan));
            await _unitOfWork.SaveAsync();
            assetDB.MarkDomainEventsAsCommitted();

            return true;
        }

        /// <summary>
        /// Deletes an asset asynchronously
        /// </summary>
        /// <param name="id">The ID of the asset to delete</param>
        /// <returns>A boolean indicating whether the asset was deleted successfully</returns>
        public async Task<bool> DeleteAsync(Guid id)
        {
            Asset? asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);
            if (asset == null)
            {
                return false;
            }
            else
            {
                _context.Assets.Remove(asset);
                await _unitOfWork.SaveAsync();
                return true;
            }
        }
    }
}
