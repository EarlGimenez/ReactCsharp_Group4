using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationsController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationsController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        
        /// Get all notifications
        
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<NotificationViewModel>> GetAllNotifications()
        {
            try
            {
                var notifications = _notificationService.GetAllNotifications();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Get unread notifications
        
        [HttpGet("unread")]
        [AllowAnonymous]
        public ActionResult<IEnumerable<NotificationViewModel>> GetUnreadNotifications()
        {
            try
            {
                var notifications = _notificationService.GetUnreadNotifications();
                return Ok(notifications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Get unread count
        
        [HttpGet("unread/count")]
        [AllowAnonymous]
        public ActionResult<int> GetUnreadCount()
        {
            try
            {
                var count = _notificationService.GetUnreadCount();
                return Ok(new { count });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Get notification by ID
        
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<NotificationViewModel> GetNotification(Guid id)
        {
            try
            {
                var notification = _notificationService.GetNotificationById(id);
                if (notification == null)
                {
                    return NotFound(new { message = "Notification not found" });
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Create a notification
        
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateNotification([FromBody] CreateNotificationViewModel model)
        {
            try
            {
                _notificationService.CreateNotification(model);
                return Ok(new { message = "Notification created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Mark notification as read
        
        [HttpPut("{id}/read")]
        [AllowAnonymous]
        public ActionResult MarkAsRead(Guid id)
        {
            try
            {
                _notificationService.MarkAsRead(id);
                return Ok(new { message = "Notification marked as read" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Mark all notifications as read
        
        [HttpPut("read-all")]
        [AllowAnonymous]
        public ActionResult MarkAllAsRead()
        {
            try
            {
                _notificationService.MarkAllAsRead();
                return Ok(new { message = "All notifications marked as read" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        
        /// Delete notification
        
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult DeleteNotification(Guid id)
        {
            try
            {
                _notificationService.DeleteNotification(id);
                return Ok(new { message = "Notification deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }
    }
}
