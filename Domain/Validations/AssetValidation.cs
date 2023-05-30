using Domain.Assets.Models;
using FluentValidation;
using FluentValidation.Results;

namespace Domain.Validations
{
    /// <summary>
    /// Validation class for the Asset entity.
    /// </summary>
    public class AssetValidation : AbstractValidator<Asset>
    {
        public static List<string> Errors = new List<string>();

        public AssetValidation()
        {
            Errors.Clear();

            // Validation rule for the Name field.
            RuleFor(x => x.Name)
                .MinimumLength(5)
                .WithMessage("The field cannot be less than 5 characters");

            // Validation rule for the PurchaseDate.
            RuleFor(x => x.PurchaseDate)
                .Must(OneYearPurchaseDate)
                .WithMessage("The date of purchase is greater than one year");

            // Validation rule for the DepartmentMail field.
            RuleFor(x => x.DepartmentMail)
                .NotEmpty().NotNull().EmailAddress()
                .WithMessage("Invalid email");
        }

        /// <summary>
        /// Checks if the purchase date is greater than one year.
        /// </summary>
        /// <param name="purchaseDate">The date of purchase to be validated.</param>
        /// <returns>True if the purchase date is greater than one year, false otherwise.</returns>
        public bool OneYearPurchaseDate(DateTime purchaseDate)
        {
            if (DateTime.Now.AddDays(-730) <= purchaseDate)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Validates the Asset and returns a list of errors.
        /// </summary>
        /// <param name="asset">The Asset to be validated.</param>
        /// <returns>List of validation errors.</returns>
        public static List<string> ValidateOk(Asset asset)
        {
            ValidationResult validationResults = asset.ValidateAsset();

            if (!validationResults.IsValid)
            {
                foreach (var failure in validationResults.Errors)
                {
                    Errors.Add(failure.ErrorMessage);
                }

                return Errors;
            }
            else
            {
                return Errors; // If there are no errors, it returns the empty list.
            }
        }
    }
}
