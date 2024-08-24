using LagQueueApplication.EFContexts;
using Microsoft.EntityFrameworkCore;

namespace LagQueueApplication.Interfaces
{
    public interface IBaseRepository
    {
        LagQueueContext DbContext { get; }

        void Add<T>(T entity);
        void AddRange<T>(IList<T> entities) where T : class;
        void Update<T>(T entity);

        bool IsTracking<T>(T entity) where T : class;
        void SaveChanges();

        DbSet<T> Get<T>(Func<T, bool>? where = null) where T : class;
    }
}
