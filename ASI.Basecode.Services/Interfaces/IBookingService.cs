using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IBookingService
    {
        IEnumerable<BookingViewModel> GetAllBookings();
        IEnumerable<BookingViewModel> GetBookingsByUserId(Guid userId);
        BookingViewModel GetBookingById(Guid bookingId);
        BookingViewModel CreateBooking(CreateBookingViewModel model);
        void UpdateBooking(Guid bookingId, UpdateBookingViewModel model);
        void DeleteBooking(Guid bookingId);
        bool CheckBookingConflict(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime, Guid? excludeBookingId = null);
    }
}
