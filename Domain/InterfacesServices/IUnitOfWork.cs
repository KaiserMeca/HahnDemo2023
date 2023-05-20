namespace Domain.InterfacesServices
{
    public interface IUnitOfWork
    {
        IAssetRepository AssetRepository { get; }
        Task SaveAsync();
    }
}
