using Shared.Model;
using Domain.Assets.Aggregates;
using Domain.Assets.Aggregates.Events;
using Domain.Validations;
using FluentValidation.Results;

namespace Domain.Assets
{
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

        public State State { get; private set; }

        public RemainingLifespan RemainingLifespan { get; set; }    
       
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
                State = State.healthy
            };
            return asset;
        }

        public new void AddEvent(IDomainEvent eve)
        {
            AddDomainEvent(eve);
        }

        public void ApplyUpdateAssetData(UpdateAssetData eventData)
        {
            if (Name != eventData.Name || DepartmentMail != eventData.DepartmentMail || Department != eventData.Department || 
                PurchaseDate != eventData.PurchaseDate || Lifespan != eventData.LifeSpan)
            {
                Name = eventData.Name;
                DepartmentMail = eventData.DepartmentMail;
                Department = eventData.Department;
                PurchaseDate = eventData.PurchaseDate;
                Lifespan = eventData.LifeSpan;
            }
        }
        public ValidationResult ValidateAsset()
        {
            return new AssetValidation().Validate(this);
        }
    }
}
