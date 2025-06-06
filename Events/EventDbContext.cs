using Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Events
{
    public class EventDbContext : DbContext
    {
        public EventDbContext(DbContextOptions<EventDbContext> options) : base(options) { }

        public virtual DbSet<Event> Events { get; set; }
        public virtual DbSet<TicketType> TicketTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(e => e.TicketTypes)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            base.OnModelCreating(modelBuilder);
        }
    }
}
