using System.Linq.Expressions;

namespace PROMART.SISTRAN.Repository.Base
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {

        void Delete(object id);
        void Delete(TEntity entityToDelete);
        List<TEntity> GetAll(Expression<Func<TEntity, bool>> filter = null, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null, string includeProperties = "");
        TEntity GetById(object id);
        void Insert(TEntity entity);
        void Update(TEntity entityToUpdate);
    }
}
