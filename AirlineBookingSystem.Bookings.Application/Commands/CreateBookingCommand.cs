using MediatR;

namespace AirlineBookingSystem.Bookings.Application.Commands
{
    public record CreateBookingCommand(Guid FlightId, string Name, string SeatNumber) : IRequest<Guid>;
}
