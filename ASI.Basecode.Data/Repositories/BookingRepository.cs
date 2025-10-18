using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Booking> GetBookings()
        {
            return this.GetDbSet<Booking>()
                .Include(b => b.Room)
                .Include(b => b.User);
        }

        public Booking GetBookingById(Guid bookingId)
        {
            return this.GetDbSet<Booking>()
                .Include(b => b.Room)
                .Include(b => b.User)
                .FirstOrDefault(b => b.BookingID == bookingId);
        }

        public IQueryable<Booking> GetBookingsByUserId(Guid userId)
        {
            return this.GetDbSet<Booking>()
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => b.UserID == userId);
        }

        public IQueryable<Booking> GetBookingsByRoomId(Guid roomId)
        {
            return this.GetDbSet<Booking>()
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => b.RoomID == roomId);
        }

        public IQueryable<Booking> GetBookingsByDateRange(DateTime startDate, DateTime endDate)
        {
            return this.GetDbSet<Booking>()
                .Include(b => b.Room)
                .Include(b => b.User)
                .Where(b => b.BookingDate >= startDate && b.BookingDate <= endDate);
        }

        public void AddBooking(Booking booking)
        {
            this.GetDbSet<Booking>().Add(booking);
            UnitOfWork.SaveChanges();
        }

        public void UpdateBooking(Booking booking)
        {
            this.GetDbSet<Booking>().Update(booking);
            UnitOfWork.SaveChanges();
        }

        public void DeleteBooking(Booking booking)
        {
            this.GetDbSet<Booking>().Remove(booking);
            UnitOfWork.SaveChanges();
        }

        public bool BookingExists(Guid bookingId)
        {
            return this.GetDbSet<Booking>().Any(b => b.BookingID == bookingId);
        }
    }
}
