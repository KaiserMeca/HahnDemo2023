using AutoMapper;
using Domain.InterfacesServices;
using Domain.Assets.Aggregates.Events;
using Domain.Validations;
using Domain.Assets.Models;
using Domain.Assets.ValueObjectModels;

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
            var assets = await _repository.GetAllAsync();
            var assetDTOs = _mapper.Map<IEnumerable<AssetDTO>>(assets);

            foreach (var assetDTO in assetDTOs)
            {
                assetDTO.RemainingLifespan = Asset.CreateRemainingLifespan(assetDTO.PurchaseDate, assetDTO.Lifespan);
                if (assetDTO.RemainingLifespan.RemainingDuration == "Timed out")
                {
                    assetDTO.State = State.Broke;
                }
                else
                {
                    assetDTO.State = State.healthy;
                }
            }

            return assetDTOs;
        }

        //public async Task<IEnumerable<AssetDTO>> GetAllAsync()
        //{
        //    var assetsList = await _repository.GetAllAsync();

        //    foreach (var item in assetsList)
        //    {
        //        item.RemainingLifespan = Asset.CreateRemainingLifespan(item.PurchaseDate, item.Lifespan);
        //    }

        //    var ListAssetDTOs = _mapper.Map<IEnumerable<AssetDTO>>(assetsList);
        //    return ListAssetDTOs;
        //}

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
                Asset asset = Asset.CreateNew(assetMap.Name, assetMap.Department, assetMap.DepartmentMail, assetMap.PurchaseDate, assetMap.Lifespan);
                asset.AddEvent(new NotifyAssetAdded(assetMap.Name, assetMap.Department, assetMap.DepartmentMail));
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
            var validationErrors = AssetValidation.ValidateOk(asset); 
            if (validationErrors.Count > 0)
            {
                return false;
            }

            asset.AddEvent(new UpdateAssetData());
            return await _repository.UpdateAsync(id, asset);
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            return await _repository.DeleteAsync(id);
        }
    }
}
