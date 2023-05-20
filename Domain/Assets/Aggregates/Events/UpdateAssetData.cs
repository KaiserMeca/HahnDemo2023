using Shared.Model;

namespace Domain.Assets.Aggregates.Events
{
    public class UpdateAssetData : IDomainEvent
    {
        public string Name { get; private set; }
        public string DepartmentMail { get; private set; }
        public UpdateAssetData(string name, string departmentMail)
        {
            Name = name;
            DepartmentMail = departmentMail;
        }
        public UpdateAssetData()
        {
        }
    }
}
