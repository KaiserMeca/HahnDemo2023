using AutoMapper;
using Domain.Repositoy;
using Domain.Security;
using Domain.Security.Agregate;
using Domain.UnitOfWork;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure
{
    public class AssetRepository : IAssetRepository
    {
        //private readonly IUnitOfWork _unitOfWork;
        private readonly AssetContext _context;

        private readonly IMapper _mapper;

        public AssetRepository(AssetContext context/*, IUnitOfWork unitOfWork*/, IMapper mapper)
        {
            _context = context;
            //_unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> AddAsync(AssetDTO assetDTO)
        {
            if (assetDTO == null || await _context.Assets.AnyAsync(x => x.Id == assetDTO.Id))
            {
                return false;
            }
            else
            {
                var assetMapper = _mapper.Map<Asset>(assetDTO);
                await _context.Assets.AddAsync(assetMapper);
                await _context.SaveChangesAsync();
                //await _unitOfWork.SaveAsync();
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
            Asset? asset = await _context.Assets.FirstOrDefaultAsync(a => a.Id == id);
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
            else if (await _context.Assets.AnyAsync(x => x.Id == asset.Id && x.Name.ToLower() != asset.Name.ToLower()))
            {
                return false;
            }
            else
            {
                Asset? assetInDataBase = await _context.Assets.FindAsync(assetDB.Id);

                // Check for unconfirmed domain events in the asset
                var uncommittedDomainEvents = asset.GetUncommittedDomainEvents();

                foreach (var domainEvent in uncommittedDomainEvents)
                {
                    if (domainEvent is UpdateAssetData updateAssetData)
                    {
                        assetInDataBase.ApplyUpdateAssetData(new UpdateAssetData(asset.Name, asset.DepartmentMail));

                    }
                }
                // Mark domain events as confirmed
                asset.MarkDomainEventsAsCommitted();
                //await _unitOfWork.SaveAsync();
                return true;
            }
        }

        public async Task<bool> countryExist(string country)
        {
            throw new NotImplementedException();
        }
    }
}
