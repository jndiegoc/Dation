namespace DationBus.Business.Dtos
{
    public class CreateMessageDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public InsurancePaymentDto insurancePayment { get; set; }
    }
    public class InsurancePaymentDto
    {
        public int PaymentId { get; set; }
        public DateTime PaymentDatetime { get; set; }
        public string Franchise { get; set; }
        public string Currency { get; set; }
        public int Amount { get; set; }
    }

    public class CreateBusMessageDto : CreateMessageDto
    {
        public DateTime ProcessDate { get; set; }
        public string AuthorName { get; set; }
    }
}
