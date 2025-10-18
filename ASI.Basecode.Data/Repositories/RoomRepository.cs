using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Data.Models;
using Basecode.Data.Repositories;
using System;
using System.Linq;

namespace ASI.Basecode.Data.Repositories
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        public RoomRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }

        public IQueryable<Room> GetRooms()
        {
            return this.GetDbSet<Room>();
        }

        public Room GetRoomById(Guid roomId)
        {
            return this.GetDbSet<Room>().FirstOrDefault(r => r.RoomID == roomId);
        }

        public void AddRoom(Room room)
        {
            this.GetDbSet<Room>().Add(room);
            UnitOfWork.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            this.GetDbSet<Room>().Update(room);
            UnitOfWork.SaveChanges();
        }

        public void DeleteRoom(Room room)
        {
            this.GetDbSet<Room>().Remove(room);
            UnitOfWork.SaveChanges();
        }

        public bool RoomExists(Guid roomId)
        {
            return this.GetDbSet<Room>().Any(r => r.RoomID == roomId);
        }
    }
}
