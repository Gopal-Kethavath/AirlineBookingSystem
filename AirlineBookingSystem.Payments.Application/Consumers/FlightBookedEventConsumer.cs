using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Payments.Application.Commands;
using MassTransit;
using MassTransit.Mediator;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMediator = MediatR.IMediator;

namespace AirlineBookingSystem.Payments.Application.Consumers
{
    public class FlightBookedEventConsumer : IConsumer<FlightBookedEvent>
    {
        private readonly IMediator _mediator;
        public FlightBookedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<FlightBookedEvent> context)
        {
            var flightBookedEvent = context.Message;
            var command = new ProcessPaymentCommand(flightBookedEvent.bookingId, 5000.00m);
            await _mediator.Send(command);
        }
    }
}
