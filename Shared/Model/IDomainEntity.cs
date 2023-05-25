namespace Shared.Model
{
    public interface IDomainEntity
    {
        Guid? Id { get; }
        DateTime PurchaseDate { get; }
        int Lifespan { get; }   
    }
}
