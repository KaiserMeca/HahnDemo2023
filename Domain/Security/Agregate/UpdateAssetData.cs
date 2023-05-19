using Common.Model;

namespace Domain.Security.Agregate
{
    public class UpdateAssetData : IDomainEvent
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string DepartmentMail { get; private set; }
        public UpdateAssetData(string name, string departmentMail)
        {
            Name = name;
            DepartmentMail = departmentMail;    
        }
    }
}
