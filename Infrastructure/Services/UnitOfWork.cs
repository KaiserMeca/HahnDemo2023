using Domain.InterfacesServices;
using Infrastructure.DataBase;
using Infrastructure.Services;
using Shared.DomainEvent;
using Shared.Model;
/// <summary>
/// Represents a unit of work for managing database transactions and repositories
/// </summary>
public class UnitOfWork : IUnitOfWork
{
    private AssetContext _context;
    private IAssetRepository? _assetRepository;
    private bool _disposed = false;
    private IDomainEventBus _domainEventBus { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="UnitOfWork"/> class
    /// </summary>
    /// <param name="context">The asset context.</param>
    /// <param name="domainEventBus">The domain event bus for executing domain events</param>
    public UnitOfWork(AssetContext context, IDomainEventBus domainEventBus)
    {
        _context = context;
        _domainEventBus = domainEventBus;
    }

    /// <summary>
    /// Gets the asset repository
    /// </summary>
    public IAssetRepository AssetRepository
    {
        get
        {
            return _assetRepository = _assetRepository ?? new AssetRepository(_context, this);
        }
    }

    /// <summary>
    /// Saves changes to the database asynchronously and executes domain events
    /// </summary>
    /// <param name="_agregateRoot">The aggregate root to access the domain events</param>
    /// <returns>A task representing the asynchronous save operation</returns>
    public async Task SaveAsync(AgregateRoot _agregateRoot)
    {
        if (!_disposed)
        {
            await _context.SaveChangesAsync();
            ExecuteDomainEvents(_agregateRoot);
            _agregateRoot.MarkDomainEventsAsCommitted();
        }
    }

    /// <summary>
    /// Saves changes to the database asynchronously
    /// </summary>
    /// <returns>A task representing the asynchronous save operation</returns>
    public async Task SaveAsync()
    {
        if (!_disposed)
        {
            await _context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// Disposes the unit of work and releases any resources used
    /// </summary>
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

    /// <summary>
    /// Executes the uncommitted domain events of an aggregate root
    /// </summary>
    /// <param name="_agregateRoot">The aggregate root containing the uncommitted domain events</param>
    private void ExecuteDomainEvents(AgregateRoot _agregateRoot)
    {
        var _uncommittedDomainEvents = _agregateRoot.GetUncommittedDomainEvents();
        foreach (var domainEvent in _uncommittedDomainEvents)
        {
            var _domainEvent = (dynamic)Convert.ChangeType(domainEvent, domainEvent.GetType());
            _domainEventBus.Execute(_domainEvent);
        }
    }

}
