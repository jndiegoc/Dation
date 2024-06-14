using DationBus.Business.Database;
using DationBus.Domain.Models;

namespace DationBus.Infrastructure.Database
{
    public class QueueLogRepository : GenericRepository<QueueLog>, IQueueLogRepository
    {
        public QueueLogRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
