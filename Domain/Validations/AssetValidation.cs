using Domain.Assets;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validations
{
    public class AssetValidation : AbstractValidator<Asset>
    {
        public static List<string> errors = new List<string>();

        public AssetValidation()
        {
            errors.Clear();
            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("The field cannot be less than 5 characters");

            RuleFor(x => x.PurchaseDate)
                .Must(OneYearPurchaseDate)
                .WithMessage("The date of purchase is greater than one year");

            RuleFor(x => x.DepartmentMail)
                .NotEmpty().NotNull().EmailAddress()
                .WithMessage("invalid mail");
        }
        /// <summary>
        /// Checks if the purchase date is greater than one year.
        /// </summary>
        /// <param name="purchaseDate">The date of purchase to be validated.</param>
        /// <returns>True if the purchase date is greater than one year, false otherwise.</returns>
        public bool OneYearPurchaseDate(DateTime purchaseDate)
        {
            if (DateTime.Now.AddDays(-365) <= purchaseDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public static List<string> ValidateOk(Asset asset)
        {
            
            ValidationResult validationResults = asset.ValidateModel();
            if (!validationResults.IsValid)
            {
                foreach (var failure in validationResults.Errors)
                {
                    errors.Add(failure.ErrorMessage);
                }

                return errors;
            }
            else
            {
                return errors;//return empty List
            }
        }
    }
}
