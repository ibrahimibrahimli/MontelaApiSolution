using Application.DTOs.AnalyticsDashboardDtos;
using Application.DTOs.PeakHourDtos;
using Application.DTOs.SystemPerformanceReportDtos;
using Application.DTOs.UserEngagementReportDtos;
using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IAnalyticsService
    {
        Task TrackNotificationSentAsync(Guid userId, NotificationType type, CancellationToken cancellationToken = default);
        Task TrackNotificationDeliveredAsync(Guid userId, NotificationType type, CancellationToken cancellationToken = default);
        Task TrackNotificationReadAsync(Guid userId, NotificationType type, TimeSpan readTime, CancellationToken cancellationToken = default);
        Task TrackNotificationFailedAsync(Guid userId, NotificationType type,string errorCode, CancellationToken cancellationToken = default);

        Task<AnalyticsDashboardDto> GetDashboardDataAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
        Task<UserEngagementReportDto> GetUserEngagementReportAsync(Guid userId, DateTime fromDate, DateTime toDate, CancellationToken cancellationToken= default);
        Task<SystemPerformanceReportDto> GetSystemPerformanceReportAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<PeakHourAnalysisDto>> GetPeakHoursAnalysisAsync(DateTime fromDate, DateTime toDate, CancellationToken cancellationToken = default);
    }
}
