using AutoMapper;
using Domain.Assets.Aggregates.Events;
using Domain.Assets.Models;
using Domain.Assets.ValueObjectModels;
using Domain.InterfacesServices;
using Domain.Validations;
using Infrastructure.EventHandlers;

/// <summary>
/// Provides asset-related services
/// </summary>
public class AssetServices : IAssetService
{
    private readonly IAssetRepository _repository;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the <see cref="AssetServices"/> class
    /// </summary>
    /// <param name="repository">The asset repository</param>
    /// <param name="mapper">The mapper for object mapping</param>
    public AssetServices(IAssetRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    /// <summary>
    /// Retrieves all assets asynchronously
    /// </summary>
    /// <returns>A collection of <see cref="AssetDTO"/></returns>
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

    /// <summary>
    /// Retrieves an asset by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the asset.</param>
    /// <returns>The <see cref="AssetDTO"/> with the specified ID</returns>
    public async Task<AssetDTO> GetForIdAsync(Guid id)
    {
        var asset = await _repository.GetForIdAsync(id);
        asset.RemainingLifespan = RemainingLifespan.CreateNew(asset.PurchaseDate, asset.Lifespan);
        AssetDTO assetDTO = _mapper.Map<AssetDTO>(asset);
        return assetDTO;
    }

    /// <summary>
    /// Adds a new asset asynchronously.
    /// </summary>
    /// <param name="assetDTO">The asset DTO to add.</param>
    /// <returns><c>true</c> if the asset was added successfully; otherwise, <c>false</c>.</returns>
    public Task<bool> AddAsync(AssetDTO assetDTO)
    {
        var assetMap = _mapper.Map<Asset>(assetDTO);
        if (AssetValidation.ValidateOk(assetMap).Count == 0)
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

    /// <summary>
    /// Updates an existing asset asynchronously.
    /// </summary>
    /// <param name="id">The ID of the asset to update.</param>
    /// <param name="assetDTO">The updated asset DTO.</param>
    /// <returns><c>true</c> if the asset was updated successfully; otherwise, <c>false</c></returns>
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

    /// <summary>
    /// Deletes an asset by its ID asynchronously
    /// </summary>
    /// <param name="id">The ID of the asset to delete.</param>
    /// <returns><c>true</c> if the asset was deleted successfully; otherwise, <c>false</c></returns>
    public async Task<bool> DeleteAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

    public async Task<bool> SendMail(string mail)
    {
        if (!NotificationEmailAddedAssetEventHandler.SendMail)
        {
            return true;
        }
        return false;
    }
}
