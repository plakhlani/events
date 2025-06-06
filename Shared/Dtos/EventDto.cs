using System.ComponentModel.DataAnnotations;

namespace Shared.Dtos
{
    public class EventDto
    {
        public long Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public virtual DateTimeOffset Date { get; set; }

        [Required]
        public required string Venue { get; set; }

        [Required]
        public required int Capacity { get; set; }

        public List<TicketTypeDto>? TicketTypes { get; set; }
    }
}
