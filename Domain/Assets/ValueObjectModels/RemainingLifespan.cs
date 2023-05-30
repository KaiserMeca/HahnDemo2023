using Shared.Model;

namespace Domain.Assets.ValueObjectModels
{
    /// <summary>
    /// Represents the remaining lifespan value object
    /// </summary>
    public class RemainingLifespan : ValueObject, IDomainEntity
    {
        public DateTime PurchaseDate { get; private set; }

        public int Lifespan { get; private set; }

        public string? RemainingDuration { get; private set; }

        /// <summary>
        /// Creates a new instance of RemainingLifespan with the specified purchase date and lifespan
        /// </summary>
        /// <param name="purchaseDate">The purchase date of the asset</param>
        /// <param name="lifespan">The lifespan in years</param>
        /// <returns>A new instance of RemainingLifespan</returns>
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

        /// <summary>
        /// Retrieves the atomic values of the remaining lifespan
        /// </summary>
        /// <returns>An enumeration of the atomic values</returns>
        public override IEnumerable<object> GetAtomicValues()
        {
            yield return PurchaseDate;
            yield return Lifespan;
            yield return RemainingDuration;
        }

        #region ValidateTimedOut
        /// <summary>
        /// Validates if the remaining lifespan has timed out
        /// </summary>
        /// <param name="y">The remaining years</param>
        /// <param name="m">The remaining months</param>
        /// <param name="d">The remaining days</param>
        /// <returns>A string representing the remaining duration or "Timed out"</returns>
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
