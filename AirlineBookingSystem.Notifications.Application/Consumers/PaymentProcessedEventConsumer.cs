using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using AirlineBookingSystem.Notifications.Application.Commands;
using AirlineBookingSystem.Notifications.Application.Interfaces;
using MassTransit;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.Notifications.Application.Consumers
{
    public class PaymentProcessedEventConsumer : IConsumer<PaymentProcessedEvent>
    {
        private readonly IMediator _mediator;

        public PaymentProcessedEventConsumer(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task Consume(ConsumeContext<PaymentProcessedEvent> context)
        {
            var paymwentProcessedEvent = context.Message;
            var message = $"Payment of {paymwentProcessedEvent.amount} for Booking Id {paymwentProcessedEvent.bookingId} was processed successfully.";
            var command = new SendNotificationCommand("masstransitmicroservicedemo@yopmail.com", message,"Email");
            await _mediator.Send(command);
        }
    }
}