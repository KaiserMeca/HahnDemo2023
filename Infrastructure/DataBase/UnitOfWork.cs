using AutoMapper;
using Domain.Repositoy;
using Domain.UnitOfWork;
using Infrastructure;


namespace Infrastructure.DataBase
{
    public class UnitOfWork : IUnitOfWork
    {
        private AssetContext _context;
        private IAssetRepository? _assetRepository;
        private bool _hasError;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class with the specified context and logger.
        /// </summary>
        /// <param name="context">The AssetContext to be used by the UnitOfWork.</param>
        /// <param name="logger">The ILogger to be used by the UnitOfWork.</param>
        public UnitOfWork(AssetContext context, IMapper mapper)
        {
            _context = context;
            _hasError = false;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets the AssetRepository associated with the UnitOfWork.
        /// </summary>
        public IAssetRepository AssetRepository
        {
            get
            {
                return _assetRepository = _assetRepository ?? new AssetRepository(_context, _mapper);
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
