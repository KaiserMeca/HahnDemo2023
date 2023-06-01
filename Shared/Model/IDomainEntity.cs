namespace Shared.Model
{
    public interface IDomainEntity
    {
        DateTime PurchaseDate { get; }
        int Lifespan { get; }   
    }
}
