using Shared.Model;

namespace Domain.Assets
{
    public class RemainingLifespan : IDomainEntity
    {
        public Guid Id { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int Lifespan { get; private set; }
        public string? RemainingDuration { get; private set; }
        
        public static RemainingLifespan CreateNew(DateTime purchaseDate, int lifespan)
        {
            DateTime expirationDate = purchaseDate.AddYears(lifespan);
            TimeSpan remainingLifespan = expirationDate - DateTime.Now;

            int remainingYears = remainingLifespan.Days / 365;
            int remainingMonths = (remainingLifespan.Days % 365) / 30;
            int remainingDays = remainingLifespan.Days % 30;


            return new RemainingLifespan
            {
                Id = Guid.NewGuid(),
                PurchaseDate = purchaseDate,
                Lifespan = lifespan,
                RemainingDuration = ValidateTimedOut(remainingYears ,remainingMonths , remainingDays)
            };
        }
        #region ValidateTimedOut
        private static string ValidateTimedOut(int y, int m, int d)
        {
            string remainingDurationReturn = "";

            if (y == 0 && m == 0 && d == 0)
            {
                return "Timed out";
            }
            if (y < 0 || m < 0 || d < 0)
            {
                return "Timed out";
            }
            if (remainingDurationReturn == "")
            {
                if (y == 0)
                {
                    return "   " + m + "m " + d + "d";
                }
                if (m == 0)
                {
                    return "   " + "   " + d + "d";
                }
                return y + "y " + m + "m " + d + "d";
            }

            return remainingDurationReturn;
        }
        #endregion
    }
}

