using System.Linq.Expressions;

namespace Shared.Model
{
    public interface IGenericRepository<T> where T : AgregateRoot
    {
        T GetById(Guid id);
        T Find(Expression<Func<T, bool>> match);
        IEnumerable<T> FindAll(Expression<Func<T, bool>> match);
        void Add(T entity);
        void Update(T entity);
        void Delete(Guid id);
    }
}
