using Common.Model;

namespace Domain.Security
{
    public class RemainingLifespan : IDomainEntity
    {
        public DateTime PurchaseDate { get; private set; }

        public int Lifespan { get; private set; }

        protected RemainingLifespan()
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
