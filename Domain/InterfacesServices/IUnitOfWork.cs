using Shared.Model;

namespace Domain.InterfacesServices
{
    public interface IUnitOfWork
    {
        Task SaveAsync(AgregateRoot aggregateRoot);
        Task SaveAsync();
        public void Dispose();
    }
}
