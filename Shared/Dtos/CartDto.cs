namespace Tickets.Dtos
{
    public class CartDto
    {
        public long CartId { get; set; }
        public long EventId { get; set; }
        public long TicketTypeId { get; set; }
        public long UserId { get; set; }
        public int Quantity { get; set; }
    }
}
