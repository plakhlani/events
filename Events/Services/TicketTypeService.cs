using Events.Models;
using Microsoft.EntityFrameworkCore;

namespace Events.Services
{
    public class TicketTypeService : ITicketTypeService
    {
        private readonly EventDbContext _context;

        public TicketTypeService(EventDbContext context)
        {
            _context = context;
        }

        public async Task<List<TicketType>> GetAll()
        {
            return await _context.TicketTypes.ToListAsync();
        }

        public async Task<TicketType?> GetbyId(long id)
        {
            return await _context.TicketTypes.FindAsync(id);
        }

        public async Task<TicketType> Add(TicketType ticketType)
        {
            _context.TicketTypes.Add(ticketType);
            await _context.SaveChangesAsync();
            return ticketType;
        }

        public async Task<TicketType?> Update(TicketType ticketType)
        {
            var existing = await _context.TicketTypes.FindAsync(ticketType.Id);
            if (existing == null) return null;

            existing.TypeName = ticketType.TypeName;
            existing.Price = ticketType.Price;
            await _context.SaveChangesAsync();
            return existing;
        }

        public async Task<bool> Delete(long id)
        {
            var ticketType = await _context.TicketTypes.FindAsync(id);
            if (ticketType == null) return false;

            _context.TicketTypes.Remove(ticketType);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}