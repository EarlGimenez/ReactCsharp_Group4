using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.Services.Services
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<NotificationViewModel> GetAllNotifications()
        {
            var notifications = _repository.GetNotifications()
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return notifications.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationID,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                IsRead = n.IsRead,
                RelatedEntityId = n.RelatedEntityID,
                CreatedAt = n.CreatedAt,
                CreatedBy = n.CreatedBy
            });
        }

        public IEnumerable<NotificationViewModel> GetUnreadNotifications()
        {
            var notifications = _repository.GetNotifications()
                .Where(n => !n.IsRead)
                .OrderByDescending(n => n.CreatedAt)
                .ToList();

            return notifications.Select(n => new NotificationViewModel
            {
                NotificationId = n.NotificationID,
                Title = n.Title,
                Message = n.Message,
                Type = n.Type,
                IsRead = n.IsRead,
                RelatedEntityId = n.RelatedEntityID,
                CreatedAt = n.CreatedAt,
                CreatedBy = n.CreatedBy
            });
        }

        public NotificationViewModel GetNotificationById(Guid id)
        {
            var notification = _repository.GetNotificationById(id);
            if (notification == null) return null;

            return new NotificationViewModel
            {
                NotificationId = notification.NotificationID,
                Title = notification.Title,
                Message = notification.Message,
                Type = notification.Type,
                IsRead = notification.IsRead,
                RelatedEntityId = notification.RelatedEntityID,
                CreatedAt = notification.CreatedAt,
                CreatedBy = notification.CreatedBy
            };
        }

        public void CreateNotification(CreateNotificationViewModel model)
        {
            var notification = new Notification
            {
                NotificationID = Guid.NewGuid(),
                Title = model.Title,
                Message = model.Message,
                Type = model.Type,
                IsRead = false,
                RelatedEntityID = model.RelatedEntityId,
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = model.CreatedBy ?? "System"
            };

            _repository.AddNotification(notification);
        }

        public void MarkAsRead(Guid notificationId)
        {
            _repository.MarkAsRead(notificationId);
        }

        public void MarkAllAsRead()
        {
            _repository.MarkAllAsRead();
        }

        public int GetUnreadCount()
        {
            return _repository.GetUnreadCount();
        }

        public void DeleteNotification(Guid notificationId)
        {
            var notification = _repository.GetNotificationById(notificationId);
            if (notification != null)
            {
                _repository.DeleteNotification(notification);
            }
        }
    }
}
