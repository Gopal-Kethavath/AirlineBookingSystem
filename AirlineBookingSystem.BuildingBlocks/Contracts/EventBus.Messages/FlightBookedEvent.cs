using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages
{
    public record FlightBookedEvent(Guid bookingId, Guid flightId, string name, string seatNumber, DateTime bookingDate);
    
}
