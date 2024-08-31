using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisInfra.Repositories
{
    public class BaseRepository : IBaseRepository
    {
        public LagQueueContext DbContext => _dbContext;

        public readonly LagQueueContext _dbContext;

        public BaseRepository(LagQueueContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Add<T>(T entity)
        {
            if (entity is null)
                return;

            DbContext.Add(entity);
        }

        public void AddRange<T>(IList<T> entities) where T : class
        {
            if (!entities.Any())
                return;

            DbContext.Set<T>().AddRange(entities);
        }

        public void Update<T>(T entity)
        {
            if (entity is null)
                return;

            DbContext.Update(entity);
        }



        public void SaveChanges() => DbContext.SaveChanges();

        public bool IsTracking<T>(T entity) where T : class
        {
            return DbContext.ChangeTracker.Entries<T>().Any(e => e.Entity == entity);
        }


        public DbSet<T> Get<T>(Func<T, bool>? where = null) where T : class
        {
            return _dbContext.Set<T>();
        }
    }
}
