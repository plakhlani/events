using Events.Models;
using Microsoft.AspNetCore.Mvc;

namespace Events.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketTypesController : Controller
    {
        private readonly Services.ITicketTypeService _service;
        public TicketTypesController(Services.ITicketTypeService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ticketTypes = await _service.GetAll();
            return Ok(ticketTypes);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            var ticketType = await _service.GetbyId(id);
            if (ticketType == null) return NotFound();
            return Ok(ticketType);
        }
        [HttpPost()]
        public async Task<IActionResult> Create(TicketType input)
        {
            var created = await _service.Add(input);
            return Ok(created);
        }

        [HttpPut]
        public async Task<IActionResult> Update(TicketType input)
        {
            var updated = await _service.Update(input);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            var deleted = await _service.Delete(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
