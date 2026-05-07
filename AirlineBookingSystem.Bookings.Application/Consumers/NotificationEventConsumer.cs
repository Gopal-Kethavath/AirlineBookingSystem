using AirlineBookingSystem.BuildingBlocks.Contracts.EventBus.Messages;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.Bookings.Application.Consumers
{
    public class NotificationEventConsumer : IConsumer<NotificationEvent>
    {
        public Task Consume(ConsumeContext<NotificationEvent> context)
        {
            var notificationEvent = context.Message;
            Console.WriteLine($"Received notification event: \"Recipient\" = {notificationEvent.recipient}, \"Message\" = {notificationEvent.message}, \"Type\" = {notificationEvent.type}");
            return Task.CompletedTask;
        }
    }
}
