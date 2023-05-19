using Common.Model;
using Domain.Security.Agregate;

namespace Domain.Security
{
    public class Asset : AgregateRoot, IDomainEntity
    {
        protected Asset()
        {

        }
        public Guid Id { get; private set; }

        public string Name { get; private set; }

        public Department Department { get; private set; }  

        public string DepartmentMail { get; private set; }

        public DateTime PurchaseDate { get; private set; }

        public int Lifespan { get; private set; }

        public State state { get; private set; }

        public RemainingLifespan RemainingLifespan { get; private set; }    
       
        public static Asset CreateNew(string name, Department department, string departmentMail, DateTime purchaseDate, int lifespan)
        {
            Asset asset = new Asset()
            {
                Id = new Guid(),
                Name = name,
                Department = department,
                DepartmentMail = departmentMail,
                PurchaseDate = purchaseDate,
                Lifespan = lifespan,
                state = State.healthy
            };
            return asset;
        }

        public void ApplyUpdateAssetData(UpdateAssetData eventData)
        {
            if (Name != eventData.Name || DepartmentMail != eventData.DepartmentMail)
            {
                Name = eventData.Name;
                DepartmentMail = eventData.DepartmentMail;
                AddDomainEvent(eventData);
            }
        }

    }
}
