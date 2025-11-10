using System;

namespace ASI.Basecode.Services.ServiceModels
{
    public class BookingViewModel
    {
        public Guid BookingId { get; set; }  
        public Guid RoomId { get; set; }     
        public Guid UserId { get; set; }     
        public string Title { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string RecurrenceRule { get; set; }
        
        // Additional fields for frontend
        public string RoomName { get; set; }
        public string UserName { get; set; }
    }
    
    public class CreateBookingViewModel
    {
        public Guid RoomId { get; set; }     
        public Guid UserId { get; set; }     
        public string Title { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string RecurrenceRule { get; set; }
    }
    
    public class UpdateBookingViewModel
    {
        public Guid? RoomId { get; set; }
        public string Title { get; set; }
        public string BookingDate { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string Description { get; set; }
        public string RecurrenceRule { get; set; }
    }
}
