using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class NotificationRepository : BaseRepository, INotificationRepository
    {
        public NotificationRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Notification> GetNotifications()
        {
            return this.GetDbSet<Notification>();
        }

        public Notification GetNotificationById(Guid id)
        {
            return this.GetDbSet<Notification>().FirstOrDefault(x => x.NotificationID == id);
        }

        public void AddNotification(Notification notification)
        {
            this.GetDbSet<Notification>().Add(notification);
            UnitOfWork.SaveChanges();
        }

        public void UpdateNotification(Notification notification)
        {
            this.GetDbSet<Notification>().Update(notification);
            UnitOfWork.SaveChanges();
        }

        public void DeleteNotification(Notification notification)
        {
            this.GetDbSet<Notification>().Remove(notification);
            UnitOfWork.SaveChanges();
        }

        public void MarkAsRead(Guid notificationId)
        {
            var notification = GetNotificationById(notificationId);
            if (notification != null)
            {
                notification.IsRead = true;
                UpdateNotification(notification);
            }
        }

        public void MarkAllAsRead()
        {
            var unreadNotifications = GetNotifications().Where(n => !n.IsRead).ToList();
            foreach (var notification in unreadNotifications)
            {
                notification.IsRead = true;
            }
            UnitOfWork.SaveChanges();
        }

        public int GetUnreadCount()
        {
            return GetNotifications().Count(n => !n.IsRead);
        }
    }
}
