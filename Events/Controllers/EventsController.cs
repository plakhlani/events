using Events.Models;
using Events.Services;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EventsController : Controller
    {
        private readonly IEventService _eventService;

        public EventsController(IEventService service)
        {
            _eventService = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var events = await _eventService.GetAll();
            return Ok(events);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var ev = await _eventService.GetById(id);
            if (ev == null) return NotFound();
            return Ok(ev);
        }

        [HttpPost()]
        public async Task<IActionResult> Create(Event input)
        {
            var created = await _eventService.Add(input);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(Event input)
        {
            var updated = await _eventService.Update(input);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _eventService.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
