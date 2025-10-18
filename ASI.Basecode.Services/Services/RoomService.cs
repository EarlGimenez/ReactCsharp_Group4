using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.Services.Services
{
    public class RoomService : IRoomService
    {
        private readonly IRoomRepository _roomRepository;

        public RoomService(IRoomRepository roomRepository)
        {
            _roomRepository = roomRepository;
        }

        public IEnumerable<RoomViewModel> GetAllRooms()
        {
            var rooms = _roomRepository.GetRooms().ToList();
            return rooms.Select(r => new RoomViewModel
            {
                RoomId = r.RoomID,
                Name = r.Name,
                Location = r.Location,
                TimeStart = r.TimeStart.ToString(@"hh\:mm"),
                TimeEnd = r.TimeEnd.ToString(@"hh\:mm"),
                Purpose = r.Purpose,
                ImageUrl = r.ImageURL
            });
        }

        public RoomViewModel GetRoomById(Guid roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null) return null;

            return new RoomViewModel
            {
                RoomId = room.RoomID,
                Name = room.Name,
                Location = room.Location,
                TimeStart = room.TimeStart.ToString(@"hh\:mm"),
                TimeEnd = room.TimeEnd.ToString(@"hh\:mm"),
                Purpose = room.Purpose,
                ImageUrl = room.ImageURL
            };
        }

        public void CreateRoom(RoomViewModel model)
        {
            var room = new Room
            {
                RoomID = Guid.NewGuid(),
                Name = model.Name,
                Location = model.Location,
                TimeStart = TimeSpan.Parse(model.TimeStart),
                TimeEnd = TimeSpan.Parse(model.TimeEnd),
                Purpose = model.Purpose,
                ImageURL = model.ImageUrl,
                CreatedAt = DateTimeOffset.Now,
                CreatedBy = "System",
                ModifiedAt = DateTimeOffset.Now,
                ModifiedBy = "System"
            };

            _roomRepository.AddRoom(room);
        }

        public void UpdateRoom(RoomViewModel model)
        {
            var room = _roomRepository.GetRoomById(model.RoomId);
            if (room == null) throw new Exception("Room not found");

            room.Name = model.Name;
            room.Location = model.Location;
            room.TimeStart = TimeSpan.Parse(model.TimeStart);
            room.TimeEnd = TimeSpan.Parse(model.TimeEnd);
            room.Purpose = model.Purpose;
            room.ImageURL = model.ImageUrl;
            room.ModifiedAt = DateTimeOffset.Now;
            room.ModifiedBy = "System";

            _roomRepository.UpdateRoom(room);
        }

        public void DeleteRoom(Guid roomId)
        {
            var room = _roomRepository.GetRoomById(roomId);
            if (room == null) throw new Exception("Room not found");

            _roomRepository.DeleteRoom(room);
        }
    }
}
