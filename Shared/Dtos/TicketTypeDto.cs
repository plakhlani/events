namespace Shared.Dtos
{
    public class TicketTypeDto
    {
        public long Id { get; set; }
        public long? EventId { get; set; }
        public required string TypeName { get; set; }
        public decimal Price { get; set; }
        public int Available { get; set; }
    }
}
