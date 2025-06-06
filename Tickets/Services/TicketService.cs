using Microsoft.EntityFrameworkCore;
using Shared.Interfaces;
using Tickets.Dtos;
using Tickets.Models;

namespace Tickets.Services
{
    public class TicketService : ITicketService
    {
        private readonly TicketDbContext _ticketDbContext;
        private readonly ITicketTypeService _ticketTypeService;

        public TicketService(TicketDbContext ticketDbContext, ITicketTypeService ticketTypeService)
        {
            _ticketDbContext = ticketDbContext;
            _ticketTypeService = ticketTypeService;
        }


        public async Task<Cart> ReserveTickets(CartDto cartDto)
        {
            var validTime = DateTime.UtcNow.AddMinutes(15);
            var ticketType = await _ticketTypeService.GetByIdEventType(cartDto.TicketTypeId);

            if (ticketType == null) throw new Exception("Invalid ticket type");

            int reservedCount = await _ticketDbContext.Carts
                .Where(c => c.TicketTypeId == cartDto.TicketTypeId && !c.IsCancelled && c.ValidTime > DateTime.UtcNow)
            .SumAsync(c => c.Quantity);

            int purchasedCount = await _ticketDbContext.Orders
                .Where(o => o.TicketTypeId == cartDto.TicketTypeId)
                .SumAsync(o => o.Quantity);

            int available = ticketType.Available - reservedCount - purchasedCount;

            if (available < cartDto.Quantity)
                throw new Exception("Not enough tickets available.");

            var lineTotal = ticketType.Price * cartDto.Quantity;
            var taxAmount = lineTotal * 0.18m; // 18% tax
            var total = lineTotal + taxAmount;

            var cart = new Cart
            {
                EventId = cartDto.EventId,
                TicketTypeId = cartDto.TicketTypeId,
                UserId = cartDto.UserId,
                Quantity = cartDto.Quantity,
                LineTotal = lineTotal,
                TaxAmount = taxAmount,
                TotalAmount = total,
                IsReserved = true,
                ReservationTime = DateTime.UtcNow,
                ValidTime = validTime
            };

            _ticketDbContext.Carts.Add(cart);
            await _ticketDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task<Cart> UpdateReserveTickets(CartDto cartDto)
        {
            var now = DateTime.UtcNow;
            var cart = await _ticketDbContext.Carts.FindAsync(cartDto.CartId);

            if (cart == null)
                throw new Exception("Cart not found.");

            if (cart.IsCancelled)
                throw new Exception("Reservation already cancelled.");

            if (cart.ValidTime < now)
                throw new Exception("Reservation has expired.");

            // Load ticket type
            var ticketType = await _ticketTypeService.GetByIdEventType(cart.TicketTypeId);
            if (ticketType == null)
                throw new Exception("Invalid ticket type.");

            // Calculate updated availability
            int reservedCount = await _ticketDbContext.Carts
                .Where(c => c.TicketTypeId == cart.TicketTypeId && !c.IsCancelled && c.ValidTime > now && c.Id != cart.Id)
                .SumAsync(c => c.Quantity);

            int purchasedCount = await _ticketDbContext.Orders
                .Where(o => o.TicketTypeId == cart.TicketTypeId)
                .SumAsync(o => o.Quantity);

            int available = ticketType.Available - reservedCount - purchasedCount;

            if (available < cartDto.Quantity)
                throw new Exception("Not enough tickets available.");

            // Update values
            var lineTotal = ticketType.Price * cartDto.Quantity;
            var taxAmount = lineTotal * 0.18m;
            var total = lineTotal + taxAmount;

            cart.Quantity = cartDto.Quantity;
            cart.LineTotal = lineTotal;
            cart.TaxAmount = taxAmount;
            cart.TotalAmount = total;
            cart.ValidTime = now.AddMinutes(15);
            _ticketDbContext.Carts.Update(cart);
            await _ticketDbContext.SaveChangesAsync();

            return cart;
        }

        public async Task CancelReservationTicket(long cartId)
        {
            var cart = await _ticketDbContext.Carts.FindAsync(cartId);

            if (cart == null || cart.IsCancelled) return;

            cart.IsCancelled = true;
            cart.CancelTime = DateTime.UtcNow;
            _ticketDbContext.Carts.Update(cart);
            await _ticketDbContext.SaveChangesAsync();
        }


        public async Task<Guid> PurchaseTicket(long cartId, int paymentType)
        {
            var cart = await _ticketDbContext.Carts.FindAsync(cartId);

            if (cart == null || cart.IsCancelled || cart.ValidTime < DateTime.UtcNow)
                throw new Exception("Reservation expired or not found");

            var ticketNo = Guid.NewGuid();

            var order = new Order
            {
                CartId = cart.Id,
                EventId = cart.EventId,
                TicketTypeId = cart.TicketTypeId,
                UserId = cart.UserId,
                Quantity = cart.Quantity,
                TaxAmount = cart.TaxAmount,
                TotalAmount = cart.TotalAmount,
                PaymentAmount = cart.TotalAmount,
                PaymentDate = DateTime.UtcNow,
                PaymentType = paymentType,
                TicketNo = ticketNo
            };

            _ticketDbContext.Orders.Add(order);
            await _ticketDbContext.SaveChangesAsync();

            return ticketNo;
        }
    }
}
