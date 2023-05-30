using Domain.InterfacesServices;
using Domain.Assets.Models;

namespace Infrastructure.DataBase
{
    public class DbDataFake
    {
        private readonly AssetContext _context;
        private readonly IUnitOfWork _unitOfWork;


        public DbDataFake(AssetContext context, IUnitOfWork unitOfWork)
        {
            _context = context;
            _unitOfWork = unitOfWork;
        }

        // This method seeds the database with test data.
        /// <summary>
        /// Seeds the database with test data.
        /// </summary>
        public void SeedData()
        {
            var random = new Random();
            var assets = new List<Asset>
            {
                Asset.CreateNew(
                    "Computer monitor",
                    Department.HQ,
                    "mail1@department1.com",
                    DateTime.UtcNow.Date.AddDays(-random.Next(1, 365)),
                    5
                ),
                Asset.CreateNew(
                    "keyboard",
                    Department.Store3,
                    "mail2@department2.com",
                    DateTime.UtcNow.Date.AddDays(-random.Next(1, 365)),
                    3
                ),
                Asset.CreateNew(
                    "Headphones",
                    Department.Store1,
                    "mail3@department3.com",
                    DateTime.UtcNow.Date.AddDays(-382),
                    1
                ),
                Asset.CreateNew(
                    "Mouse",
                    Department.MaintenanceStation,
                    "mail4@department4.com",
                    DateTime.UtcNow.Date.AddDays(-635),
                    2
                )};

            _context.Assets.AddRange(assets);
            _unitOfWork.SaveAsync();
        }
    }
}
