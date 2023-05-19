using Domain.Repositoy;

namespace Domain.UnitOfWork
{
    public interface IUnitOfWork
    {
        IAssetRepository AssetRepository { get; }
        Task SaveAsync();
    }
}
