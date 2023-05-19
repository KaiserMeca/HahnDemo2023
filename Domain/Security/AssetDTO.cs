﻿using Common.Model;
using Domain.Security.Agregate;

namespace Domain.Security
{
    public class AssetDTO : AgregateRoot
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Department Department { get; set; }
        public string DepartmentMail { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int Lifespan { get; set; }
        public State State { get; set; }
        public RemainingLifespan RemainingLifespan { get; set; }
    }
}