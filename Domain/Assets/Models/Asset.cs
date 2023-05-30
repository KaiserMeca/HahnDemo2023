using Shared.Model;
using Domain.Assets.Aggregates.Events;
using Domain.Validations;
using FluentValidation.Results;
using Shared.DomainEvent;
using System.ComponentModel.DataAnnotations.Schema;
using Domain.Assets.ValueObjectModels;

namespace Domain.Assets.Models
{
    /// <summary>
    /// Represents an asset entity as an aggregate root and as a domain entity
    /// </summary>
    public class Asset : AgregateRoot, IDomainEntity
    {
        protected Asset()
        {

        }

        public Guid? Id { get; private set; }

        public string Name { get; private set; }

        public Department Department { get; private set; }

        public string DepartmentMail { get; private set; }

        public DateTime PurchaseDate { get; private set; }

        public int Lifespan { get; private set; }

        private State State { get; set; }

        [NotMapped]
        public RemainingLifespan RemainingLifespan { get; set; }

        /// <summary>
        /// Represents the responsibility of creating a new asset
        /// </summary>
        /// <param name="name">The name of the asset</param>
        /// <param name="department">The department of the asset</param>
        /// <param name="departmentMail">The department mail of the asset</param>
        /// <param name="purchaseDate">The purchase date of the asset</param>
        /// <param name="lifespan">The lifespan of the asset</param>
        /// <returns>Returns the new asset created</returns>
        public static Asset CreateNew(string name, Department department, string departmentMail, DateTime purchaseDate, int lifespan)
        {
            Asset asset = new Asset()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Department = department,
                DepartmentMail = departmentMail,
                PurchaseDate = purchaseDate,
                Lifespan = lifespan,
                State = State.healthy,
                RemainingLifespan = new RemainingLifespan()
            };

            return asset;
        }

        /// <summary>
        /// Creates a new instance of the RemainingLifespan class
        /// </summary>
        /// <param name="purchaseDate">The purchase date of the asset.</param>
        /// <param name="lifeSpan">The lifespan of the asset</param>
        /// <returns>A new object of the RemainingLifespan class</returns>
        public static RemainingLifespan CreateRemainingLifespan(DateTime purchaseDate, int lifeSpan)
        {
            RemainingLifespan remainingLifespan = RemainingLifespan.CreateNew(purchaseDate, lifeSpan);

            return remainingLifespan;
        }

        /// <summary>
        /// Add a new domain event for assets
        /// </summary>
        /// <param name="eve">The domain event to add</param>
        public new void AddEvent(IDomainEvent eve)
        {
            AddDomainEvent(eve);
        }

        /// <summary>
        /// Applies an update to the asset's data based on the provided event data
        /// </summary>
        /// <param name="eventData">The event data containing the updated asset data</param>
        public void ApplyUpdateAssetData(UpdateAssetData eventData)
        {
            if (Name != eventData.Name || DepartmentMail != eventData.DepartmentMail || Department != eventData.Department || PurchaseDate != eventData.PurchaseDate || Lifespan != eventData.LifeSpan)
            {
                Name = eventData.Name;
                DepartmentMail = eventData.DepartmentMail;
                Department = eventData.Department;
                PurchaseDate = eventData.PurchaseDate;
                Lifespan = eventData.LifeSpan;
            }
        }

        /// <summary>
        /// Validates the asset
        /// </summary>
        /// <returns>The Fluentvalidation result for the asset</returns>
        public ValidationResult ValidateAsset()
        {
            return new AssetValidation().Validate(this);
        }
    }
}
