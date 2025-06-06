using Microsoft.AspNetCore.Mvc;
using Tickets.Dtos;
using Tickets.Services;

namespace Tickets.Controllers
{
    public class TicketController : Controller
    {
        private readonly ITicketService _ticketService;

        public TicketController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpPost("Reserve")]
        public async Task<IActionResult> ReserveTickets(CartDto cartDto)
        {
            var reserveTicket = await _ticketService.ReserveTickets(cartDto);
            return Ok(reserveTicket);
        }

        [HttpPut("Reserve")]
        public async Task<IActionResult> UpdateReserveTickets(CartDto cartDto)
        {
            var reserveTicket = await _ticketService.UpdateReserveTickets(cartDto);
            return Ok(reserveTicket);
        }

        [HttpPut("Cancel")]
        public async Task<IActionResult> CancelReservationTicket(long cartId)
        {
            await _ticketService.CancelReservationTicket(cartId);
            return Ok();
        }

        [HttpPost("Purchase")]
        public async Task<IActionResult> PurchaseTicket(long cartId, int paymentType)
        {
            var ticketNo = await _ticketService.PurchaseTicket(cartId, paymentType);
            return Ok(new { TicketNo = ticketNo });
        }
    }
}

