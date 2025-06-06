using Events.Models;

namespace Events.Services
{
    public interface IEventService
    {
        Task<List<Event>> GetAll();
        Task<Event?> GetById(long id);
        Task<Event> Add(Event createEvent);
        Task<Event?> Update(Event updatedEvent);
        Task<bool> Delete(long id);
    }
}
