using ASI.Basecode.Data.Models;
using System;
using System.Linq;

namespace ASI.Basecode.Data.Interfaces
{
    public interface INotificationRepository
    {
        IQueryable<Notification> GetNotifications();
        Notification GetNotificationById(Guid id);
        void AddNotification(Notification notification);
        void UpdateNotification(Notification notification);
        void DeleteNotification(Notification notification);
        void MarkAsRead(Guid notificationId);
        void MarkAllAsRead();
        int GetUnreadCount();
    }
}
