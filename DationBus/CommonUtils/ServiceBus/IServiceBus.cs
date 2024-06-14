namespace CommonUtils.ServiceBus
{
    public interface IServiceBus
    {
        Task SendPlainTextMessageAsync(string messageBody);
        Task<string> ReceiveMessageAsync();
    }
}
