using ASI.Basecode.Data.Interfaces;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ASI.Basecode.Services.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoomRepository _roomRepository;
        private readonly IBookingRepository _bookingRepository;

        public DashboardService(
            IUserRepository userRepository,
            IRoomRepository roomRepository,
            IBookingRepository bookingRepository)
        {
            _userRepository = userRepository;
            _roomRepository = roomRepository;
            _bookingRepository = bookingRepository;
        }

        public DashboardStatisticsViewModel GetDashboardStatistics()
        {
            var users = _userRepository.GetUsers().ToList();
            var rooms = _roomRepository.GetRooms().ToList();
            var bookings = _bookingRepository.GetBookings().ToList();

            // Calculate total and active users
            var totalUsers = users.Count;
            var activeUsers = users.Count(u => u.IsActive);

            // Get bookings from the last 30 days to determine "active" users
            var thirtyDaysAgo = DateTime.Now.AddDays(-30);
            var recentBookingUserIds = bookings
                .Where(b => b.CreatedAt >= thirtyDaysAgo)
                .Select(b => b.UserID)
                .Distinct()
                .Count();

            // Use recent booking users as active users if available
            if (recentBookingUserIds > 0)
            {
                activeUsers = recentBookingUserIds;
            }

            // Calculate room statistics
            var totalRooms = rooms.Count;

            // Calculate room usage rate - percentage of rooms with bookings in the last 30 days
            var roomsWithRecentBookings = bookings
                .Where(b => b.CreatedAt >= thirtyDaysAgo)
                .Select(b => b.RoomID)
                .Distinct()
                .Count();

            var roomUsageRate = totalRooms > 0 
                ? Math.Round((decimal)roomsWithRecentBookings / totalRooms * 100, 2) 
                : 0;

            // Calculate bounce rate (simulated - percentage of single-time users)
            var userBookingCounts = bookings
                .GroupBy(b => b.UserID)
                .Select(g => new { UserId = g.Key, Count = g.Count() })
                .ToList();

            var singleBookingUsers = userBookingCounts.Count(u => u.Count == 1);
            var totalBookingUsers = userBookingCounts.Count();
            var bounceRate = totalBookingUsers > 0 
                ? Math.Round((decimal)singleBookingUsers / totalBookingUsers * 100, 2) 
                : 0;

            // Generate monthly user activity for the last 12 months
            var userActivity = GenerateMonthlyUserActivity(users, bookings);

            // Generate room usage by type (Purpose)
            var roomUsageByType = rooms
                .GroupBy(r => r.Purpose)
                .Select(g => new RoomUsageByType
                {
                    Name = g.Key ?? "Unspecified",
                    Value = g.Count()
                })
                .OrderByDescending(r => r.Value)
                .ToList();

            // If no rooms have purposes, provide a default
            if (!roomUsageByType.Any())
            {
                roomUsageByType.Add(new RoomUsageByType
                {
                    Name = "All Rooms",
                    Value = totalRooms
                });
            }

            return new DashboardStatisticsViewModel
            {
                TotalUsers = totalUsers,
                ActiveUsers = activeUsers,
                TotalRooms = totalRooms,
                TotalBookings = bookings.Count,
                BounceRate = bounceRate,
                RoomUsageRate = roomUsageRate,
                UserActivity = userActivity,
                RoomUsageByType = roomUsageByType
            };
        }

        private List<MonthlyUserActivity> GenerateMonthlyUserActivity(
            List<Data.Models.User> users, 
            List<Data.Models.Booking> bookings)
        {
            var result = new List<MonthlyUserActivity>();
            var now = DateTime.Now;

            for (int i = 11; i >= 0; i--)
            {
                var monthDate = now.AddMonths(-i);
                var monthStart = new DateTime(monthDate.Year, monthDate.Month, 1);
                var monthEnd = monthStart.AddMonths(1).AddDays(-1);

                // Count users created by this month
                var totalUsersByMonth = users.Count(u => u.CreatedAt.DateTime <= monthEnd);

                // Count active users in this month (users with bookings)
                var activeUsersInMonth = bookings
                    .Where(b => b.BookingDate >= monthStart && b.BookingDate <= monthEnd)
                    .Select(b => b.UserID)
                    .Distinct()
                    .Count();

                result.Add(new MonthlyUserActivity
                {
                    Name = monthDate.ToString("MMM"),
                    Active = activeUsersInMonth,
                    Total = totalUsersByMonth > 0 ? totalUsersByMonth : users.Count
                });
            }

            return result;
        }
    }
}
