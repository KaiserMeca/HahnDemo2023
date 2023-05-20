using Domain.InterfacesServices;
using Domain.Assets;

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
                    Domain.Assets.Aggregates.Department.HQ,
                    "mail1@department1.com",
                    DateTime.UtcNow.Date.AddDays(-random.Next(1, 365)),
                    5
                ),
                Asset.CreateNew(
                    "keyboard",
                    Domain.Assets.Aggregates.Department.Store3,
                    "mail2@department2.com",
                    DateTime.UtcNow.Date.AddDays(-random.Next(1, 365)),
                    2
                ),
                Asset.CreateNew(
                    "Mouse",
                    Domain.Assets.Aggregates.Department.MaintenanceStation,
                    "mail3@department3.com",
                    DateTime.UtcNow.Date.AddDays(-random.Next(1, 365)),
                    2
                )};

            _context.Assets.AddRange(assets);
            _unitOfWork.SaveAsync();
        }
    }
}
