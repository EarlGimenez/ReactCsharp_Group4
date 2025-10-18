using System;

namespace ASI.Basecode.Services.ServiceModels
{
    public class RoomViewModel
    {
        public Guid RoomId { get; set; }  
        public string Name { get; set; }
        public string Location { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public string Purpose { get; set; }
        public string ImageUrl { get; set; }  
    }
}
