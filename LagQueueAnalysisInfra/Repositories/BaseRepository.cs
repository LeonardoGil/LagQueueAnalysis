using LagQueueAnalysisInfra.EFContexts;
using LagQueueAnalysisInfra.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LagQueueAnalysisInfra.Repositories
{
    public class BaseRepository<Context> : IBaseRepository<Context> where Context : DbContext
    {
        public Context DbContext => _dbContext;

        public readonly Context _dbContext;

        public BaseRepository(Context dbContext)
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


        public IQueryable<T> Get<T>(Func<T, bool>? where = null) where T : class
        {
            if (where is not null)
                return _dbContext.Set<T>().Where(where).AsQueryable();

            return _dbContext.Set<T>().AsQueryable();
        }
    }
}
