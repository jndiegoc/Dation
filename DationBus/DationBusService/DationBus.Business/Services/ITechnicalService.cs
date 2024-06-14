using DationBus.Business.Dtos;

namespace DationBus.Business.Services
{
    public interface ITechnicalService
    {
        public Task CreateQueueMessage(CreateMessageDto createMessageDto);
    }
}
