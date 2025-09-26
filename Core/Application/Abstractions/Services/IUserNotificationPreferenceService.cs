using Domain.Enums;

namespace Application.Common.Interfaces
{
    public interface IUserNotificationPreferenceService
    {
        Task<IEnumerable<Guid>> GetActiveUsersWithNotificationTypeEnabledAsync(NotificationType type, CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> GetActiveUsersWithMotivationEnabledAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> GetActiveUsersWithHealthRemindersEnabledAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> GetActiveUsersWithMarketingEnabledAsync(CancellationToken cancellationToken = default);
        Task<IEnumerable<Guid>> GetMarketTrendSubscribersAsync(CancellationToken cancellationToken = default);
        Task<bool> IsUserOptedInForNotificationTypeAsync(Guid userId, NotificationType type, CancellationToken cancellationToken = default);
        Task<TimeSpan?> GetUserOptimalNotificationTimeAsync(Guid userId, CancellationToken cancellationToken = default);
    }
}
