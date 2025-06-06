using Shared.Dtos;

namespace Shared.Interfaces
{
    public interface IEventService
    {
        Task<List<EventDto>> GetAllEvents();
        Task<EventDto?> GetByIdEvent(long id);
        Task<EventDto> CreateEvent(EventDto createEvent);
        Task<EventDto?> UpdateEvent(EventDto updatedEvent);
        Task<bool> DeleteEvent(long id);
    }
}
