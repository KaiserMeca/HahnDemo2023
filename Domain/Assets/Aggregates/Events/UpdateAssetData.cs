using Shared.Model;

namespace Domain.Assets.Aggregates.Events
{
    public class UpdateAssetData : IDomainEvent
    {
        public string Name { get; private set; }
        public string DepartmentMail { get; private set; }
        public Department Department { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int LifeSpan { get; private set; }

        public UpdateAssetData(string name, string departmentMail, Department department, DateTime purchaseDate, int lifeSpan)
        {
            Name = name;
            DepartmentMail = departmentMail;
            Department = department;
            PurchaseDate = purchaseDate;
            LifeSpan = lifeSpan;
        }
        public UpdateAssetData()
        {
        }
    }
}
