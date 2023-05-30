using Domain.InterfacesServices;
using Domain.Assets.Aggregates.Events;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using Domain.Assets.Models;

namespace Infrastructure.Services
{
    public class AssetRepository : IAssetRepository
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly AssetContext _context;

        public AssetRepository(AssetContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(Asset asset)
        {
            if (await _context.Assets.AnyAsync(x => x.Id == asset.Id))
            {
                throw new RepeatedIdException("The user code already exist");
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

        public async Task<IEnumerable<Asset>> GetAllAsync()
        {
            var assets = await _context.Assets.ToListAsync();
            return assets ?? new List<Asset>();
        }

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
