using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.BuildingBlocks.Common
{
    public class EventBusConstants
    {
        public const string FlightBookQueue = "flight-booking-queue";
        public const string PaymentProcessedQueue = "payment-processed-queue";
        public const string NotificationQueue = "notification-queue";
    }
}
