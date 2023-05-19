using Common.Model;
using Domain.Security;
using System.Linq.Expressions;

namespace Domain.Repositoy
{
    public interface IAssetRepository : IGenericRepository<Asset>
    {
        public void Add(Asset entity);
        public void Delete(Guid id);
        public Asset Find(Expression<Func<Asset, bool>> match);
        public IEnumerable<Asset> FindAll(Expression<Func<Asset, bool>> match);
        public Asset GetById(Guid id);
        public void Update(Asset entity);
    }
}
