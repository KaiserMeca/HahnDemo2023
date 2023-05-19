namespace Common.Model
{
    public interface IDomainEntity
    {
        //Guid Id { get; }
        //string Name { get; }
        DateTime PurchaseDate { get; }
        int Lifespan { get; }   
    }
}
