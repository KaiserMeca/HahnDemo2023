using AutoMapper;
using Domain.InterfacesServices;
using Domain.Assets;
using Domain.Assets.Aggregates.Events;
using Domain.Validations;

namespace Infrastructure.Services
{
    public class AssetServices : IAssetService
    {
        private readonly IAssetRepository _repository;
        private readonly IMapper _mapper;

        public AssetServices(IAssetRepository repository, IMapper mapper )
        {
            _repository = repository;
            _mapper = mapper;
        }


        public async Task<IEnumerable<AssetDTO>> GetAllAsync()
        {
            var assetsList = await _repository.GetAllAsync();
           
            foreach (var item in assetsList)
            {
                item.RemainingLifespan = RemainingLifespan.CreateNew(item.PurchaseDate, item.Lifespan);
            }

            var ListAssetDTOs = _mapper.Map<IEnumerable<AssetDTO>>(assetsList);
            return ListAssetDTOs;
        }

        public async Task<AssetDTO> GetForIdAsync(Guid id)
        {
            var asset = await _repository.GetForIdAsync(id);
            asset.RemainingLifespan = RemainingLifespan.CreateNew(asset.PurchaseDate, asset.Lifespan);
            AssetDTO assetDTO = _mapper.Map<AssetDTO>(asset);
            return assetDTO;
        }

        public Task<bool> AddAsync(AssetDTO assetDTO)
        {
            var assetMap = _mapper.Map<Asset>(assetDTO);
            if (AssetValidation.ValidateOk(assetMap).Count == 0 )
            {
                Asset asset = Asset.CreateNew(assetMap.Name, assetMap.Department, assetMap.DepartmentMail,
                    assetMap.PurchaseDate,assetMap.Lifespan);
                return _repository.AddAsync(asset);
            }
            else
            {
                return Task.FromResult(false);
            }
        }

        public async Task<bool> UpdateAsync(Guid id, AssetDTO assetDTO)
        {
            var asset = _mapper.Map<Asset>(assetDTO);
            if (AssetValidation.ValidateOk(asset).Count == 0)
            {
                //Add DomainEvent
                UpdateAssetData updateAsset = new UpdateAssetData();
                asset.AddEvent(updateAsset);
                return await _repository.UpdateAsync(id, asset);
            }
            else
            {
                return await Task.FromResult(false);
            }
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }


        public Task<bool> countryExist(string country)
        {
            throw new NotImplementedException();
        }
    }
}
