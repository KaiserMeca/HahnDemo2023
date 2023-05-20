using AutoMapper;
using Domain.InterfacesServices;
using Domain.Repositoy;
using Infrastructure.DataBase;


namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private AssetContext _context;
        private IAssetRepository? _assetRepository;
        private bool _hasError;

       
        public UnitOfWork(AssetContext context)
        {
            _context = context;
            _hasError = false;
        }

        /// <summary>
        /// Gets the AssetRepository associated with the UnitOfWork.
        /// </summary>
        public IAssetRepository AssetRepository
        {
            get
            {
                return _assetRepository = _assetRepository ?? new AssetRepository(_context, this);
            }
        }

        /// <summary>
        /// Asynchronously saves changes made to the database by the UnitOfWork.
        /// </summary>
        /// <returns>A task that represents the asynchronous save operation.</returns>
        public async Task SaveAsync()
        {
            if (!_hasError)
            {
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Releases all resources used by the UnitOfWork.
        /// </summary>
        public async void Dispose()
        {
            try
            {
                await SaveAsync();
                _context.Dispose();
            }
            catch (Exception ex)
            {
                _hasError = true;
            }
        }
    }
}
