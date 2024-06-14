using DationBusFunction.DAL.Implementations;
using DationBusFunction.Model.DBModel;
using DationBusFunction.Model.Enums;
using Microsoft.Azure.WebJobs;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using System.Xml;

namespace DationBusFunction.Api.Functions
{
    public class QueueFunction
    {

        private ApplicationDbContext _applicationDbContext;

        [FunctionName("ReadQueue")]
        public async Task Run([ServiceBusTrigger("queue1", Connection = "AZURESERVICEBUSCONNECTIONSTRING")] string myQueueItem, ILogger log)
        {
            try
            {
                await SaveProcess(myQueueItem, RecordQueueStatus.Read);
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            }
            catch (System.Exception)
            {
                await SaveProcess(myQueueItem, RecordQueueStatus.ReadError);
                log.LogInformation($"C# ServiceBus queue trigger function processed message: {myQueueItem}");
            }
        }

        private async Task SaveProcess(string message, string recordQueueStatus)
        {
            QueueLogRepository queueLogRepository = new(applicationDbContext(Environment.GetEnvironmentVariable("DATABASECONNECTIONSTRING")), 
                new LoggerFactory().CreateLogger<QueueLogRepository>());

            QueueLog newQueueLog = new()
            {
                CreateDate = DateTime.UtcNow,
                QueueMessage = GetXMLAsString(Newtonsoft.Json.JsonConvert.DeserializeXmlNode("{\"Row\":" + message + "}", "root")),
                StatusName = recordQueueStatus
            };

            queueLogRepository.Insert(newQueueLog);
            await queueLogRepository.SaveAsync();
        }

        public string GetXMLAsString(XmlDocument myxml)
        {
            return myxml.OuterXml;
        }

        public ApplicationDbContext applicationDbContext(string dbConnectionString)
        {

            if (null != _applicationDbContext)
                _applicationDbContext = null;

            SqlConnectionStringBuilder sqlConnStrBuilder = new SqlConnectionStringBuilder(dbConnectionString);
            sqlConnStrBuilder.ConnectTimeout = 500;
            DbContextOptionsBuilder<ApplicationDbContext> dbContextOptionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            dbContextOptionsBuilder.UseSqlServer(sqlConnStrBuilder.ConnectionString);
            _applicationDbContext = new ApplicationDbContext(dbContextOptionsBuilder.Options);


            return _applicationDbContext;
        }
    }
}
