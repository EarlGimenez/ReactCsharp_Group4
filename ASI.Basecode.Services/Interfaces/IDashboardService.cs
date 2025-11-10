using ASI.Basecode.Services.ServiceModels;

namespace ASI.Basecode.Services.Interfaces
{
    
    /// Interface for dashboard statistics service
    
    public interface IDashboardService
    {
        
        /// Gets dashboard statistics including user, room, and booking metrics
        
        DashboardStatisticsViewModel GetDashboardStatistics();
    }
}
