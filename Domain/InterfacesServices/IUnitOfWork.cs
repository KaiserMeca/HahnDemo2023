using Shared.Model;

namespace Domain.InterfacesServices
{
    public interface IUnitOfWork
    {
        //IAssetRepository AssetRepository { get; }
        Task SaveAsync(AgregateRoot aggregateRoot);
        Task SaveAsync();
    }
}
