using Shared.Dtos;

namespace Shared.Interfaces
{
    public interface ITicketTypeService
    {
        Task<List<TicketTypeDto>> GetAllEventType();
        Task<TicketTypeDto?> GetByIdEventType(long id);
        Task<TicketTypeDto> CreateEventType(TicketTypeDto ticketType);
        Task<TicketTypeDto?> UpdateEventType(TicketTypeDto ticketType);
        Task<bool> DeleteEventType(long id);
    }
}
