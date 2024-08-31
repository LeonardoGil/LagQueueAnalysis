using LagQueueAnalysisInfra.EFContexts;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisInfra.Interfaces
{
    public interface IBaseRepository<Context> where Context : DbContext
    {
        Context DbContext { get; }

        void Add<T>(T entity);
        void AddRange<T>(IList<T> entities) where T : class;
        void Update<T>(T entity);

        bool IsTracking<T>(T entity) where T : class;
        void SaveChanges();

        DbSet<T> Get<T>(Func<T, bool>? where = null) where T : class;
    }
}
