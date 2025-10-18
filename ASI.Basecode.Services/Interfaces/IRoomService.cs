using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.Interfaces
{
    public interface IRoomService
    {
        IEnumerable<RoomViewModel> GetAllRooms();
        RoomViewModel GetRoomById(Guid roomId);
        void CreateRoom(RoomViewModel model);
        void UpdateRoom(RoomViewModel model);
        void DeleteRoom(Guid roomId);
    }
}
