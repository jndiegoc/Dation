namespace CommonUtils.ServiceBus
{
    public record DomainMessage
    {
        public object JsonContent { get; set; }

        public DomainMessage(object content)
        {
            JsonContent = content;
        }
    }
}
