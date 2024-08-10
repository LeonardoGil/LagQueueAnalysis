using LagQueueApplication.EFContexts;

namespace LagQueueApplication.Interfaces
{
    public interface IBaseRepository
    {
        LagQueueContext DbContext { get; }

        T Add<T>(T entity);
        T Update<T>(T entity);

        bool IsTracking<T>(T entity) where T : class;

        void SaveChanges();
    }
}
