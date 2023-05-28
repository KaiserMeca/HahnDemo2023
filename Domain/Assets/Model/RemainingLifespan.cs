using Shared.Model;

namespace Domain.Assets.Model
{
    public class RemainingLifespan : ValueObject
    {
        public DateTime PurchaseDate { get; private set; }
        public int Lifespan { get; private set; }
        public string? RemainingDuration { get; private set; }

        public static RemainingLifespan CreateNew(DateTime purchaseDate, int lifespan)
        {
            DateTime expirationDate = purchaseDate.AddYears(lifespan);
            TimeSpan remainingLifespan = expirationDate - DateTime.Now;

            int remainingYears = remainingLifespan.Days / 365;
            int remainingMonths = remainingLifespan.Days % 365 / 30;
            int remainingDays = remainingLifespan.Days % 30;


            return new RemainingLifespan
            {
                PurchaseDate = purchaseDate,
                Lifespan = lifespan,
                RemainingDuration = ValidateTimedOut(remainingYears, remainingMonths, remainingDays)
            };
        }

        public override IEnumerable<object> GetAtomicValues()
        {
            yield return PurchaseDate;
            yield return Lifespan;
            yield return RemainingDuration;
        }

        #region ValidateTimedOut
        private static string ValidateTimedOut(int y, int m, int d)
        {
            if (y == 0 && m == 0 && d == 0 || y < 0 || m < 0 || d < 0)
            {
                return "Timed out";
            }

            string remainingDurationReturn = "";

            if (y != 0)
            {
                remainingDurationReturn += $"{y}y ";
            }
            if (m != 0)
            {
                remainingDurationReturn += $"{m}m ";
            }
            remainingDurationReturn += $"{d}d";

            return remainingDurationReturn;
        }
        #endregion
    }
}

