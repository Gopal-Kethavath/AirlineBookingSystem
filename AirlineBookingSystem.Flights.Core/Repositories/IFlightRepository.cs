namespace AirlineBookingSystem.Flights.Core.Repositories
{
    public interface IFlightRepository
    {
        Task<Entities.Flights> GetFlightByIdAsync(Guid id);
        Task<Entities.Flights> AddFlightAsync(Entities.Flights flight);
        Task DeleteFlightAsync(Guid id);
    }
}
