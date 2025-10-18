using System;

namespace ASI.Basecode.Data.Models
{
    public class Booking
    {
        public Guid BookingID { get; set; }
        public Guid RoomID { get; set; }
        public Guid UserID { get; set; }
        public string Title { get; set; }
        public DateTime BookingDate { get; set; }
        public TimeSpan StartTime { get; set; }
        public TimeSpan EndTime { get; set; }
        public string Description { get; set; }
        public string RecurrenceRule { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }

        // Navigation properties
        public virtual Room Room { get; set; }
        public virtual User User { get; set; }
    }
}
