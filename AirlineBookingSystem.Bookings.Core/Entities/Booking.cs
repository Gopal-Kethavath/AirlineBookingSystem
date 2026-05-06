using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AirlineBookingSystem.Bookings.Core.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid FlightId { get; set; }
        public string Name { get; set; }
        public string SeatNumber { get; set; }
        public DateTime BookingDate { get; set; }


    }
}
