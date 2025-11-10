using System;

namespace ASI.Basecode.Data.Models
{
    public class Notification
    {
        public Guid NotificationID { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; } // "booking_created", "booking_updated", "booking_cancelled", "user_registered"
        public bool IsRead { get; set; }
        public Guid? RelatedEntityID { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }
}
