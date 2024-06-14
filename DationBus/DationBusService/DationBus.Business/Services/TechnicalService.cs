using CommonUtils.ServiceBus;
using DationBus.Business.Database;
using DationBus.Business.Dtos;
using DationBus.Domain.Enums;
using DationBus.Domain.Models;
using System.Text.Json;
using System.Xml;

namespace DationBus.Business.Services
{
    public class TechnicalService : ITechnicalService
    {
        public readonly IServiceBus _serviceBus;
        private readonly IUnitOfWork _unitOfWork;

        public TechnicalService(IServiceBus serviceBus, IUnitOfWork unitOfWork)
        {
            _serviceBus = serviceBus;
            _unitOfWork = unitOfWork;
        }

        public async Task CreateQueueMessage(CreateMessageDto createMessageDto)
        {
            CreateBusMessageDto newMessage = new() {
                AuthorName = RecordAuthor.AuthorName,
                ProcessDate = DateTime.UtcNow,
                Id = createMessageDto.Id,
                Name = createMessageDto.Name,
                Surname = createMessageDto.Surname,
                insurancePayment = createMessageDto.insurancePayment
            };

            var messageBody = JsonSerializer.Serialize(newMessage);

            try
            {
                await _serviceBus.SendPlainTextMessageAsync(messageBody);

                QueueLog newQueueLog = new() {
                    CreateDate = DateTime.UtcNow,
                    QueueMessage = GetXMLAsString(Newtonsoft.Json.JsonConvert.DeserializeXmlNode("{\"Row\":" + messageBody + "}", "root")),
                    StatusName = RecordQueueStatus.Processed
                };
                
                _unitOfWork.QueueLogRepository.Add(newQueueLog);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception)
            {
                QueueLog newQueueLog = new()
                {
                    CreateDate = DateTime.UtcNow,
                    QueueMessage = messageBody,
                    StatusName = RecordQueueStatus.WithError
                };

                _unitOfWork.QueueLogRepository.Add(newQueueLog);
                await _unitOfWork.SaveAsync();
            }
        }
        public string GetXMLAsString(XmlDocument myxml)
        {
            return myxml.OuterXml;
        }
    }
}
