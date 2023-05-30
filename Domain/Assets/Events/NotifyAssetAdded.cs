using Domain.Assets.Models;
using Shared.DomainEvent;

namespace Domain.Assets.Aggregates.Events
{
    /// <summary>
    /// Event generated when an asset is added and must be notified by mail
    /// </summary>
    public class NotifyAssetAdded : IDomainEvent
    {
        /// <summary>
        /// Gets the name of the asset.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the department of the asset.
        /// </summary>
        public Department Department { get; private set; }

        /// <summary>
        /// Gets the email of the department associated with the asset.
        /// </summary>
        public string DepartmentMail { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="NotifyAssetAdded"/> class.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <param name="department">The department of the asset.</param>
        /// <param name="departmentMail">The email of the department associated with the asset.</param>
        public NotifyAssetAdded(string name, Department department, string departmentMail)
        {
            Name = name;
            Department = department;
            DepartmentMail = departmentMail;
        }
    }
}
