using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ASI.Basecode.WebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;
        private readonly IWebHostEnvironment _environment;
        private static readonly string[] AllowedExtensions = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };
        private const long MaxFileSize = 5 * 1024 * 1024; // 5MB

        public RoomsController(IRoomService roomService, IWebHostEnvironment environment)
        {
            _roomService = roomService;
            _environment = environment;
        }

        /// 
        /// Get all rooms
        /// 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult<IEnumerable<RoomViewModel>> GetRooms()
        {
            try
            {
                var rooms = _roomService.GetAllRooms();
                return Ok(rooms);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Get room by ID
        /// 
        [HttpGet("{id}")]
        [AllowAnonymous]
        public ActionResult<RoomViewModel> GetRoom(Guid id)
        {
            try
            {
                var room = _roomService.GetRoomById(id);
                if (room == null)
                {
                    return NotFound(new { message = "Room not found" });
                }
                return Ok(room);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Create a new room
        /// 
        [HttpPost]
        [AllowAnonymous]
        public ActionResult CreateRoom([FromBody] RoomViewModel model)
        {
            try
            {
                _roomService.CreateRoom(model);
                return Ok(new { message = "Room created successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Update an existing room
        /// 
        [HttpPut("{id}")]
        [AllowAnonymous]
        public ActionResult UpdateRoom(Guid id, [FromBody] RoomViewModel model)
        {
            try
            {
                model.RoomId = id;
                _roomService.UpdateRoom(model);
                return Ok(new { message = "Room updated successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Delete a room
        /// 
        [HttpDelete("{id}")]
        [AllowAnonymous]
        public ActionResult DeleteRoom(Guid id)
        {
            try
            {
                _roomService.DeleteRoom(id);
                return Ok(new { message = "Room deleted successfully" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = ex.Message });
            }
        }

        /// 
        /// Upload room image
        /// 
        [HttpPost("upload-image")]
        [AllowAnonymous]
        public async Task<ActionResult> UploadImage(IFormFile image)
        {
            try
            {
                // Validate file
                if (image == null || image.Length == 0)
                {
                    return BadRequest(new { message = "No file uploaded" });
                }

                // Check file size
                if (image.Length > MaxFileSize)
                {
                    return BadRequest(new { message = $"File size exceeds maximum allowed size of {MaxFileSize / 1024 / 1024}MB" });
                }

                // Check file extension
                var extension = Path.GetExtension(image.FileName).ToLowerInvariant();
                if (!AllowedExtensions.Contains(extension))
                {
                    return BadRequest(new { message = $"File type not allowed. Allowed types: {string.Join(", ", AllowedExtensions)}" });
                }

                // Generate unique filename
                var fileName = $"{Guid.NewGuid()}{extension}";
                
                // Create uploads directory if it doesn't exist
                var uploadsPath = Path.Combine(_environment.WebRootPath, "uploads", "rooms");
                if (!Directory.Exists(uploadsPath))
                {
                    Directory.CreateDirectory(uploadsPath);
                }

                // Save file
                var filePath = Path.Combine(uploadsPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                // Return relative path for frontend
                var imagePath = $"/uploads/rooms/{fileName}";
                return Ok(new { imagePath });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Error uploading file: {ex.Message}" });
            }
        }
    }
}
