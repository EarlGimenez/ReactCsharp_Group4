using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace ASI.Basecode.Services.Services
{
    public class BookingService : IBookingService
    {
        private readonly IBookingRepository _bookingRepository;
        private readonly IRoomRepository _roomRepository;

        public BookingService(IBookingRepository bookingRepository, IRoomRepository roomRepository)
        {
            _bookingRepository = bookingRepository;
            _roomRepository = roomRepository;
        }

        public IEnumerable<BookingViewModel> GetAllBookings()
        {
            var bookings = _bookingRepository.GetBookings().ToList();
            return MapToViewModels(bookings);
        }

        public IEnumerable<BookingViewModel> GetBookingsByUserId(Guid userId)
        {
            var bookings = _bookingRepository.GetBookingsByUserId(userId).ToList();
            return MapToViewModels(bookings);
        }

        public BookingViewModel GetBookingById(Guid bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null) return null;

            return MapToViewModel(booking);
        }

        public BookingViewModel CreateBooking(CreateBookingViewModel model)
        {
            // Parse date and times
            var bookingDate = DateTime.Parse(model.BookingDate);
            var startTime = TimeSpan.Parse(model.StartTime);
            var endTime = TimeSpan.Parse(model.EndTime);

            // Check for conflicts
            if (CheckBookingConflict(model.RoomId, bookingDate, startTime, endTime)) 
            {
                throw new Exception("Booking conflict detected. This time slot is already booked.");
            }

            // Verify room exists
            if (!_roomRepository.RoomExists(model.RoomId)) 
            {
                throw new Exception("Room not found");
            }

            var booking = new Booking
            {
                BookingID = Guid.NewGuid(),
                RoomID = model.RoomId,     
                UserID = model.UserId,      
                Title = model.Title,
                BookingDate = bookingDate,
                StartTime = startTime,
                EndTime = endTime,
                Description = model.Description,
                RecurrenceRule = model.RecurrenceRule,
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = model.UserId.ToString(),   
                ModifiedAt = DateTimeOffset.Now,
                ModifiedBy = model.UserId.ToString()
            };

            _bookingRepository.AddBooking(booking);
            
            // Fetch the created booking with navigation properties
            var createdBooking = _bookingRepository.GetBookingById(booking.BookingID);
            return MapToViewModel(createdBooking);
        }

        public void UpdateBooking(Guid bookingId, UpdateBookingViewModel model)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            // Parse date and times if provided
            var bookingDate = string.IsNullOrEmpty(model.BookingDate) ? booking.BookingDate : DateTime.Parse(model.BookingDate);
            var startTime = string.IsNullOrEmpty(model.StartTime) ? booking.StartTime : TimeSpan.Parse(model.StartTime);
            var endTime = string.IsNullOrEmpty(model.EndTime) ? booking.EndTime : TimeSpan.Parse(model.EndTime);

            // Check for conflicts (excluding current booking)
            if (CheckBookingConflict(booking.RoomID, bookingDate, startTime, endTime, bookingId))
            {
                throw new Exception("Booking conflict detected. This time slot is already booked.");
            }

            booking.Title = model.Title ?? booking.Title;
            booking.BookingDate = bookingDate;
            booking.StartTime = startTime;
            booking.EndTime = endTime;
            booking.Description = model.Description ?? booking.Description;
            booking.RecurrenceRule = model.RecurrenceRule ?? booking.RecurrenceRule;
            booking.ModifiedAt = DateTimeOffset.Now;
            booking.ModifiedBy = booking.UserID.ToString();

            _bookingRepository.UpdateBooking(booking);
        }

        public void DeleteBooking(Guid bookingId)
        {
            var booking = _bookingRepository.GetBookingById(bookingId);
            if (booking == null) throw new Exception("Booking not found");

            _bookingRepository.DeleteBooking(booking);
        }

        public bool CheckBookingConflict(Guid roomId, DateTime date, TimeSpan startTime, TimeSpan endTime, Guid? excludeBookingId = null)
        {
            var bookings = _bookingRepository.GetBookingsByRoomId(roomId)
                .Where(b => b.BookingDate.Date == date.Date);

            if (excludeBookingId.HasValue)
            {
                bookings = bookings.Where(b => b.BookingID != excludeBookingId.Value);
            }

            var bookingsList = bookings.ToList();

            foreach (var booking in bookingsList)
            {
                // Check if time ranges overlap
                if (startTime < booking.EndTime && endTime > booking.StartTime)
                {
                    return true;
                }
            }

            return false;
        }

        private IEnumerable<BookingViewModel> MapToViewModels(List<Booking> bookings)
        {
            return bookings.Select(MapToViewModel);
        }

        private BookingViewModel MapToViewModel(Booking booking)
        {
            return new BookingViewModel
            {
                BookingId = booking.BookingID,     
                RoomId = booking.RoomID,           
                UserId = booking.UserID,           
                Title = booking.Title,
                BookingDate = booking.BookingDate.ToString("yyyy-MM-dd"),
                StartTime = booking.StartTime.ToString(@"hh\:mm"),
                EndTime = booking.EndTime.ToString(@"hh\:mm"),
                Description = booking.Description,
                RecurrenceRule = booking.RecurrenceRule,
                RoomName = booking.Room?.Name,
                UserName = booking.User?.Username ?? booking.User?.Name
            };
        }
    }
}
