using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface INotificationService
    {
        IEnumerable<NotificationViewModel> GetAllNotifications();
        IEnumerable<NotificationViewModel> GetUnreadNotifications();
        NotificationViewModel GetNotificationById(Guid id);
        void CreateNotification(CreateNotificationViewModel model);
        void MarkAsRead(Guid notificationId);
        void MarkAllAsRead();
        int GetUnreadCount();
        void DeleteNotification(Guid notificationId);
    }
}
