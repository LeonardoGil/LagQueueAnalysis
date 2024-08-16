using LagQueueApplication.EFContexts;

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
    }
}
