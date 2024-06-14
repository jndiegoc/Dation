using DationBus.Business.Database;

namespace DationBus.Infrastructure.Database
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            QueueLogRepository = new QueueLogRepository(context);
        }

        public IQueueLogRepository QueueLogRepository { get; private set; }

        public IGenericRepository<T> GenericRepository<T>() where T : class, new()
        {
            return new GenericRepository<T>(_context);
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        public int Save()
        {
            return _context.SaveChanges();
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
