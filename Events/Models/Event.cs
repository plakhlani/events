using System.ComponentModel.DataAnnotations;

namespace Events.Models
{
    public class Event
    {
        [Key]
        public long Id { get; set; }

        [Required]
        [StringLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Required]
        public virtual DateTimeOffset Date { get; set; }

        [Required]
        public required string Venue { get; set; }

        [Required]
        public required int Capacity { get; set; }

        public List<TicketType>? TicketTypes { get; set; }
    }
}
