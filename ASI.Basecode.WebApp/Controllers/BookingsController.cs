using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingsController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingsController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }

        /// 
        /// Get all bookings or filter by userId
        /// 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<BookingViewModel>> GetBookings([FromQuery] Guid? userId)
        {
            try
            {
                IEnumerable<BookingViewModel> bookings;
                
                if (userId.HasValue)
                {
                    bookings = _bookingService.GetBookingsByUserId(userId.Value);
                }
                else
                {
                    bookings = _bookingService.GetAllBookings();
                }

                return Ok(bookings);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Get booking by ID
        /// 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<BookingViewModel> GetBooking(Guid id)
        {
            try
            {
                var booking = _bookingService.GetBookingById(id);
                if (booking == null)
                {
                    return NotFound(new { message = "Booking not found" });
                }
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Create a new booking
        /// 
        [HttpPost]
        [AllowAnonymous]
        public ActionResult<BookingViewModel> CreateBooking([FromBody] CreateBookingViewModel model)
        {
            try
            {
                var booking = _bookingService.CreateBooking(model);
                return Ok(booking);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Update an existing booking
        /// 
        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult UpdateBooking(Guid id, [FromBody] UpdateBookingViewModel model)
        {
            try
            {
                _bookingService.UpdateBooking(id, model);
                return Ok(new { message = "Booking updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Delete a booking
        /// 
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult DeleteBooking(Guid id)
        {
            try
            {
                _bookingService.DeleteBooking(id);
                return Ok(new { message = "Booking deleted successfully" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        /// 
        /// Check for booking conflicts
        /// 
        [HttpPost("check-conflict")]
        [AllowAnonymous]
        public ActionResult CheckConflict([FromBody] ConflictCheckViewModel model)
        {
            try
            {
                var hasConflict = _bookingService.CheckBookingConflict(
                    model.RoomID,
                    DateTime.Parse(model.BookingDate),
                    TimeSpan.Parse(model.StartTime),
                    TimeSpan.Parse(model.EndTime),
                    model.ExcludeBookingId
                );

                return Ok(new { hasConflict });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }

    public class ConflictCheckViewModel
    {
        public Guid RoomID { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public Guid? ExcludeBookingId { get; set; }
    }
}
