using Domain.Assets.Models;
using Shared.DomainEvent;

namespace Domain.Assets.Aggregates.Events
{
    /// <summary>
    /// Event raised when the data of an asset is updated.
    /// </summary>
    public class UpdateAssetData : IDomainEvent
    {
        public string Name { get; private set; }
        public string DepartmentMail { get; private set; }
        public Department Department { get; private set; }
        public DateTime PurchaseDate { get; private set; }
        public int LifeSpan { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAssetData"/> class.
        /// </summary>
        /// <param name="name">The name of the asset.</param>
        /// <param name="departmentMail">The email of the department associated with the asset.</param>
        /// <param name="department">The department of the asset.</param>
        /// <param name="purchaseDate">The purchase date of the asset</param>
        /// <param name="lifeSpan">The lifespan of the asset.</param>
        public UpdateAssetData(string name, string departmentMail, Department department, DateTime purchaseDate, int lifeSpan)
        {
            Name = name;
            DepartmentMail = departmentMail;
            Department = department;
            PurchaseDate = purchaseDate;
            LifeSpan = lifeSpan;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateAssetData"/> class by copying the data from another instance
        /// </summary>
        /// <param name="updateAssetData">The original instance to copy the data from</param>
        public UpdateAssetData(UpdateAssetData updateAssetData)
        {
            Name = updateAssetData.Name;
            DepartmentMail = updateAssetData.DepartmentMail;
            Department = updateAssetData.Department;
            PurchaseDate = updateAssetData.PurchaseDate;
            LifeSpan = updateAssetData.LifeSpan;
        }

        /// <summary>
        /// Initializes a new, parameterless instance of the class to load into the DomainEvent list <see cref="UpdateAssetData"/>
        /// </summary>
        public UpdateAssetData()
        {
        }
    }
}
