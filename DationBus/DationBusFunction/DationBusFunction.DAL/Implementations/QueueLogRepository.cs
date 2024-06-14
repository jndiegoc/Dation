using DationBusFunction.DAL.Definitions;
using Microsoft.Extensions.Logging;
using DationBusFunction.Model.DBModel;

namespace DationBusFunction.DAL.Implementations
{
    public class QueueLogRepository : IQueueLogRepository
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<QueueLogRepository> _logger;

        public QueueLogRepository(ApplicationDbContext dbContext, ILogger<QueueLogRepository> logger)
        {
            this._dbContext = dbContext;
            this._logger = logger;
        }

        public void Insert(QueueLog queueLog)
        {
			try
			{
                _dbContext.QueueLogs.Add(queueLog);

			}
			catch (Exception exception)
			{
                _logger.LogError(exception.Message, exception);
                throw;
			}
        }

        public async Task SaveAsync()
        {
            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                _logger.LogError(exception.Message, exception);
                throw;
            }
        }
    }
}
