using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TasaheelProject.Models
{
    public class Payment
    {
        [Key]
        public Guid PaymentId { get; set; } = Guid.NewGuid();

        [Required, Range(typeof(decimal), "1", "1000")]
        public decimal Amount { get; set; }

        [Required, MaxLength(15)]
        public string PaymentMethod { get; set; }

        [Required, MaxLength(100)]
        public string TransactionId { get; set; }

        public DateTime PaidAt { get; set; } = DateTime.UtcNow;

        [ForeignKey("Request")]
        public Guid RequestId { get; set; }
        public Request Request { get; set; }
    }
}
