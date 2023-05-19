using Common.Model;

namespace Domain.Security
{
    public class RemainingLifespan : IDomainEntity
    {
        public Guid Id { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int Lifespan { get; private set; }

        public RemainingLifespan()
        {

        }
        public static DateTime CreateNew(DateTime purchaseDate, int lifespan)
        {
            DateTime expirationDate = purchaseDate.AddYears(lifespan);
            TimeSpan remainingLifespan = expirationDate - DateTime.Now;
            return new DateTime(remainingLifespan.Ticks);
        }
    }
}
