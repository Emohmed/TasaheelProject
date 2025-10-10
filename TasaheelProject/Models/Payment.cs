namespace TasaheelProject.Models
{
    public class Payment
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public decimal Amount { get; set; }
        public string PaymentMethod { get; set; }
        public string TransactionId { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        public Guid RequestId { get; set; }
        public Request Request { get; set; }
    }
}
