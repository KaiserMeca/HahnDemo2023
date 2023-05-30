using Domain.Assets.Models;
using Shared.DomainEvent;

namespace Domain.Assets.Aggregates.Events
{
    public class NotifyAssetAdded : IDomainEvent
    {
        public string Name { get; private set; }

        public Department Department { get; private set; }

        public string DepartmentMail { get; private set; }

        public NotifyAssetAdded(string name, Department department, string departmentMail)
        {
            Name = name;
            Department = department;
            DepartmentMail = departmentMail;
        }
    }
}
