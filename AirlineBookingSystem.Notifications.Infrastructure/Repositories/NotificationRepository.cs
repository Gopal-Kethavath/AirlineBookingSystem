using AirlineBookingSystem.Notifications.Core.Entities;
using AirlineBookingSystem.Notifications.Core.Repositories;
using Dapper;
using System.Data;

namespace AirlineBookingSystem.Notifications.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {

        private readonly IDbConnection _dbConnection;

        public NotificationRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task LogNotificationAsync(Notification notification)
        {
            const string sql = @"
                INSERT INTO Notifications (Id, Recipient, Message, Type)
                VALUES (@Id, @Recipient, @Message, @Type)";
            await _dbConnection.ExecuteAsync(sql, notification);
        }
    }
}
