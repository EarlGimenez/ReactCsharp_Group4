using ASI.Basecode.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.Data.Interfaces
{
    public interface IRoomRepository
    {
        IQueryable<Room> GetRooms();
        Room GetRoomById(Guid roomId);
        void AddRoom(Room room);
        void UpdateRoom(Room room);
        void DeleteRoom(Room room);
        bool RoomExists(Guid roomId);
    }
}
