using DationBusFunction.Model.DBModel;

namespace DationBusFunction.DAL.Definitions
{
    public interface IQueueLogRepository
    {
        void Insert(QueueLog queueLog);
        Task SaveAsync();
    }
}
