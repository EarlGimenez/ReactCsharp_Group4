using System;

namespace ASI.Basecode.Services.ServiceModels
{
    public class NotificationViewModel
    {
        public Guid NotificationId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public bool IsRead { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
    }

    public class CreateNotificationViewModel
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string Type { get; set; }
        public Guid? RelatedEntityId { get; set; }
        public string CreatedBy { get; set; }
    }
}
