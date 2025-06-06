using Tickets.Dtos;
using Tickets.Models;

namespace Tickets.Services
{
    public interface ITicketService
    {
        Task<Cart> ReserveTickets(CartDto cart);
        Task CancelReservationTicket(long CartId);
        Task<Guid> PurchaseTicket(long cartId, int paymentType);
        Task<Cart> UpdateReserveTickets(CartDto cart);
    }
}
