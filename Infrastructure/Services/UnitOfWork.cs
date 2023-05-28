using AutoMapper;
using Domain.InterfacesServices;
using Infrastructure.DataBase;
using Microsoft.EntityFrameworkCore.Storage;
using Shared.DomainEvent;
using Shared.Model;

namespace Infrastructure.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private AssetContext _context;
        private IAssetRepository? _assetRepository;
        private bool _disposed = false;
        private IDomainEventBus _domainEventBus { get; set; }

        public UnitOfWork(AssetContext context, IDomainEventBus domainEventBus)
        {
            _context = context;
            _domainEventBus = domainEventBus;
        }

        public IAssetRepository AssetRepository
        {
            get
            {
                return _assetRepository = _assetRepository ?? new AssetRepository(_context, this);
            }
        }

        public async Task SaveAsync(AgregateRoot _agregateRoot)
        {
            if (!_disposed)
            {
                await _context.SaveChangesAsync();
                ExecuteDomainEvents(_agregateRoot);
            }
        }
        public async Task SaveAsync()
        {
            if (!_disposed)
            {
                await _context.SaveChangesAsync();
                
            }
        }

        public void Dispose()
        {
            try
            {
                 SaveAsync();
                _context.Dispose();
            }
            catch (Exception ex)
            {
                _disposed = true;
            }
        }

        private void ExecuteDomainEvents(AgregateRoot _agregateRoot)
        {
            var _uncommittedDomainEvents = _agregateRoot.GetUncommittedDomainEvents();
            foreach (var domainEvent in _uncommittedDomainEvents)
            {
                var _domainEvent = (dynamic)Convert.ChangeType(domainEvent, domainEvent.GetType());
                _domainEventBus.Execute(_domainEvent);
            }
        }
        //    private AssetContext _context;
        //    private IAssetRepository? _assetRepository;
        //    private bool _hasError;


        //    public UnitOfWork(AssetContext context)
        //    {
        //        _context = context;
        //        _hasError = false;
        //    }

        //    /// <summary>
        //    /// Gets the AssetRepository associated with the UnitOfWork.
        //    /// </summary>
        //    public IAssetRepository AssetRepository
        //    {
        //        get
        //        {
        //            return _assetRepository = _assetRepository ?? new AssetRepository(_context, this);
        //        }
        //    }

        //    /// <summary>
        //    /// Asynchronously saves changes made to the database by the UnitOfWork.
        //    /// </summary>
        //    /// <returns>A task that represents the asynchronous save operation.</returns>
        //    public async Task SaveAsync()
        //    {
        //        if (!_hasError)
        //        {
        //            await _context.SaveChangesAsync();
        //        }
        //    }

        //    /// <summary>
        //    /// Releases all resources used by the UnitOfWork.
        //    /// </summary>
        //    public async void Dispose()
        //    {
        //        try
        //        {
        //            await SaveAsync();
        //            _context.Dispose();
        //        }
        //        catch (Exception ex)
        //        {
        //            _hasError = true;
        //        }
        //    }
    }
}
