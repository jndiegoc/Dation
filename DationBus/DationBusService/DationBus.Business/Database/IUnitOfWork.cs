namespace DationBus.Business.Database
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GenericRepository<T>() where T : class, new();
        IQueueLogRepository QueueLogRepository { get; }
        int Save();
        Task SaveAsync();
    }
}
