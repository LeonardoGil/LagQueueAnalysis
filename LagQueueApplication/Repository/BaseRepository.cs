using LagQueueApplication.EFContexts;
using LagQueueApplication.Interfaces;

namespace LagQueueApplication.Repository
{
    public class BaseRepository : IBaseRepository
    {
        public LagQueueContext DbContext => _dbContext;

        public readonly LagQueueContext _dbContext;

        public BaseRepository(LagQueueContext dbContext)
        {
            _dbContext = dbContext;
        }

        public T Add<T>(T entity)
        {
            if (entity is null)
                return entity;

            return (T)DbContext.Add(entity).Entity;
        }

        public T Update<T>(T entity)
        {
            if (entity is null)
                return entity;

            return (T)DbContext.Update(entity).Entity;
        }

        public void SaveChanges() => DbContext.SaveChanges();

        public bool IsTracking<T>(T entity) where T : class
        {
            return DbContext.ChangeTracker.Entries<T>().Any(e => e.Entity == entity); 
        }
    }
}
