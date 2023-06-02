using Domain.Assets.ValueObjectModels;
using System.Text.Json.Serialization;

namespace Domain.Assets.Models
{
    /// <summary>
    /// Represents an Asset Data Transfer Object (DTO).
    /// </summary>
    public class AssetDTO
    {
        public Guid? Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string DepartmentMail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Lifespan { get; set; }
        [JsonIgnore]
        public State State { get; set; }
        public RemainingLifespan RemainingLifespan { get; set; }
    }
}
