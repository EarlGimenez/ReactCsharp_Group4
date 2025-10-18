using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IBookingRepository
    {
        IQueryable<Booking> GetBookings();
        Booking GetBookingById(Guid bookingId);
        IQueryable<Booking> GetBookingsByUserId(Guid userId);
        IQueryable<Booking> GetBookingsByRoomId(Guid roomId);
        IQueryable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate);
        void AddBooking(Booking booking);
        void UpdateBooking(Booking booking);
        void DeleteBooking(Booking booking);
        bool BookingExists(Guid bookingId);
    }
}
