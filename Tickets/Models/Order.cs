using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class Order
    {

        [Key]
        public long Id { get; set; }
        public long CartId { get; set; }
        public long EventId { get; set; }
        public long TicketTypeId { get; set; }
        public long UserId { get; set; }
        public int Quantity { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public int PaymentType { get; set; }
        public DateTime PaymentDate { get; set; }
        public decimal PaymentAmount { get; set; }
        public Guid TicketNo { get; set; }
    }
}
