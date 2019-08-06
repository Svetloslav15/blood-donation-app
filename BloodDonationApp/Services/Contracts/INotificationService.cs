namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;

    public interface INotificationService
    {
        List<Notification> GetAllByUser(string userId);

        bool ReadAllToUser(string userId);

        Notification AddNotification(string content, string userId);

        int GetUnreadedCount(string username);
    }
}