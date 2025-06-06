using Tickets.Dtos;

namespace Shared.Interfaces
{
    public interface ITicketService
    {
        Task<CartDto> ReserveTickets(CartDto cart);
        Task<bool> CancelReservationTicket(long CartId);
        Task<Guid> PurchaseTicket(long cartId, int paymentType);
        Task<CartDto> UpdateReserveTickets(CartDto cart);
    }
}
