using Microsoft.EntityFrameworkCore;

namespace PROMART.SISTRAN.Repository.Base
{
    public interface IUnitOfWork<DBContext> : IDisposable where DBContext : DbContext
    {
        void Save();
        Task<int> SaveAsync();
    }
}