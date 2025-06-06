using Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Services
{
    public class EventService : IEventService
    {
        private readonly EventDbContext _context;
        public EventService(EventDbContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAll()
        {
            return await _context.Events.Include(e => e.TicketTypes).ToListAsync();
        }

        public async Task<Event> Add(Event evt)
        {
            _context.Events.Add(evt);
            await _context.SaveChangesAsync();
            return evt;
        }

        public async Task<Event?> GetById(long id)
        {
            return await _context.Events.Include(e => e.TicketTypes).FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Event?> Update(Event input)
        {
            var existing = await _context.Events.Include(e => e.TicketTypes).FirstOrDefaultAsync(e => e.Id == input.Id);
            if (existing == null) return null;

            existing.Name = input.Name;
            existing.Date = input.Date;
            existing.Venue = input.Venue;
            existing.Description = input.Description;
            existing.Capacity = input.Capacity;
            if (input.TicketTypes != null && input.TicketTypes.Count > 0)
            {
                foreach (var ticketTypeInput in input.TicketTypes)
                {
                    if (ticketTypeInput.Id == 0)
                    {
                        ticketTypeInput.EventId = existing?.Id;
                        _context.TicketTypes.Add(ticketTypeInput);
                    }
                    else
                    {
                        var existingTicketType = existing?.TicketTypes?.FirstOrDefault(tt => tt.Id == ticketTypeInput.Id);
                        if (existingTicketType != null)
                        {
                            existingTicketType.TypeName = ticketTypeInput.TypeName;
                            existingTicketType.Price = ticketTypeInput.Price;
                            existingTicketType.Available = ticketTypeInput.Available;
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                var inputIds = input.TicketTypes.Where(tt => tt.Id != 0).Select(tt => tt.Id).ToList();
                var ticketTypesToRemove = existing?.TicketTypes?.Where(tt => !inputIds.Contains(tt.Id)).ToList();

                _context.TicketTypes.RemoveRange(ticketTypesToRemove);
            }
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(long id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return false;
            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
