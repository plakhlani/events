using Events.Models;

namespace Events.Services
{
    public interface ITicketTypeService
    {
        Task<List<TicketType>> GetAll();
        Task<TicketType?> GetbyId(long id);
        Task<TicketType> Add(TicketType ticketType);
        Task<TicketType?> Update(TicketType ticketType);
        Task<bool> Delete(long id);
    }
}
