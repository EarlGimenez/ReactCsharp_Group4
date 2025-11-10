using System;
using System.Collections.Generic;

namespace ASI.Basecode.Services.ServiceModels
{
    
    /// View model for dashboard statistics
    
    public class DashboardStatisticsViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int TotalRooms { get; set; }
        public int TotalBookings { get; set; }
        public decimal BounceRate { get; set; }
        public decimal RoomUsageRate { get; set; }
        public List<MonthlyUserActivity> UserActivity { get; set; }
        public List<RoomUsageByType> RoomUsageByType { get; set; }
    }

    public class MonthlyUserActivity
    {
        public string Name { get; set; }
        public int Active { get; set; }
        public int Total { get; set; }
    }

    public class RoomUsageByType
    {
        public string Name { get; set; }
        public int Value { get; set; }
    }
}
