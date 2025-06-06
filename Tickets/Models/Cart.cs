using System.ComponentModel.DataAnnotations;

namespace Tickets.Models
{
    public class Cart
    {
        [Key]
        public long Id { get; set; }
        public long EventId { get; set; }
        public long TicketTypeId { get; set; }
        public long UserId { get; set; }
        public int Quantity { get; set; }
        public decimal LineTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public decimal TotalAmount { get; set; }
        public bool IsReserved { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime ReservationTime { get; set; }
        public DateTime? CancelTime { get; set; }
        public DateTime ValidTime { get; set; }
    }
}
