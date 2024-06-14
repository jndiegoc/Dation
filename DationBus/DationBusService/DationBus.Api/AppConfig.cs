using DationBus.Domain.Custom;

namespace DationBus.Api
{
    internal static class AppConfig
    {
        private const string localKeyConfigName = "LocalConfig";

        internal static LocalConfig Local { get; private set; }
        internal static CloudConfig Cloud { get; private set; }

        internal static LocalConfig ReadLocalConfig(IConfiguration configuration)
        {
            if (default == Local)
                Local = configuration.GetSection(localKeyConfigName).Get<LocalConfig>();

            return Local;
        }

        internal static CloudConfig ReadCloudConfig(IConfiguration configuration)
        {
            if (default != Cloud)
                return Cloud;

            if (default == Local)
                ReadLocalConfig(configuration);

            string dbConnectionString = Environment.GetEnvironmentVariable("DATABASECONNECTIONSTRING");

            Cloud = new()
            {
                DatabaseConnectionString = dbConnectionString,
                AzureServiceBusConnectionString = Environment.GetEnvironmentVariable("AZURESERVICEBUSCONNECTIONSTRING")
            };

            return Cloud;
        }
    }
}
