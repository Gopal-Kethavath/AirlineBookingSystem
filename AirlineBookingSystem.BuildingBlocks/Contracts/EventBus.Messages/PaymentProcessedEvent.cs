using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages
{
    public record PaymentProcessedEvent(Guid bookingId, Guid paymentId, decimal amount, DateTime paymentDate);
    
}
