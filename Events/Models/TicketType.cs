using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class TicketType
    {
        [Key]
        public long Id { get; set; }
        public long? EventId { get; set; }
        public required string TypeName { get; set; }
        public decimal Price { get; set; }
        public int Available { get; set; }
    }
}
