namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class NotificationService : INotificationService
    {
        private ApplicationDbContext dbContext;

        public NotificationService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public Notification AddNotification(string content, string userId)
        {
            Notification notification = null;
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            if (content.Trim() != "" && user != null)
            {
                notification = new Notification()
                {
                    Content = content,
                    UserId = userId,
                    IsRead = false
                };

                this.dbContext.Notifications.Add(notification);
                this.dbContext.SaveChanges();
            }
            return notification;
        }

        public List<Notification> GetAllByUser(string userId)
        {
            return this.dbContext.Notifications
                .Where(x => x.UserId == userId)
                .ToList();
        }

        public int GetUnreadedCount(string username)
        {
            return this.dbContext.Notifications
                .Where(x => x.User.UserName == username && x.IsRead == false)
                .Count();
        }

        public bool ReadAllToUser(string userId)
        {
            var notifications = this.dbContext.Notifications
                .Where(x => x.UserId == userId)
                .ToList();

            if (notifications.Count() != 0)
            {
                foreach (var item in notifications)
                {
                    item.IsRead = true;
                }
                this.dbContext.SaveChanges();
                return true;

            }
            return false;
        }
    }
}