using Domain.InterfacesServices;
using Domain.Assets;
using Domain.Assets.Aggregates.Events;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Exceptions;
using Shared.Model;

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
            var ListAssets = await _context.Assets.ToListAsync();
            if (ListAssets == null)
            {
                return new List<Asset>();
            }
            else
            {

                return ListAssets;
            }
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
            Asset? assetDB = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);

            if (assetDB == null)
            {
                return false;
            }

            else
            {
                Asset? assetInDataBase = await _context.Assets.FindAsync(assetDB.Id);

                //Check for unconfirmed domain events in the asset
                //var uncommittedDomainEvents = asset.GetUncommittedDomainEvents();

                //foreach (var domainEvent in uncommittedDomainEvents)
                //{
                //    if (domainEvent is UpdateAssetData updateAssetData)
                //    {
                //        assetInDataBase.ApplyUpdateAssetData(new UpdateAssetData(asset.Name, asset.DepartmentMail, asset.Department, asset.PurchaseDate,
                //            asset.Lifespan));
                //        await _unitOfWork.SaveAsync(asset);
                //    }
                //}
                ////Mark domain events as confirmed
                //asset.MarkDomainEventsAsCommitted();
                await _unitOfWork.SaveAsync(asset);//Eliminar al descomentar lo de arriba
                return true;

                

            }
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
                await _unitOfWork.SaveAsync(asset);
                return true;
            }
        }

        public async Task<bool> countryExist(string country)
        {
            throw new NotImplementedException();
        }
    }
}
