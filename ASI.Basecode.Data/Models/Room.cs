using System;

namespace ASI.Basecode.Data.Models
{
    public class Room
    {
        public Guid RoomID { get; set; }
        public string Name { get; set; }
        public string Location { get; set; }
        public TimeSpan TimeStart { get; set; }
        public TimeSpan TimeEnd { get; set; }
        public string Purpose { get; set; }
        public string ImageURL { get; set; }
        public DateTimeOffset CreatedAt { get; set; }
        public string CreatedBy { get; set; }
        public DateTimeOffset ModifiedAt { get; set; }
        public string ModifiedBy { get; set; }
    }
}
