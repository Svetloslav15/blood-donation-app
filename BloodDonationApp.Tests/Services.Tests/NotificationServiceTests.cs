namespace BloodDonationApp.Tests.Services.Tests
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Xunit;

    public class NotificationServiceTests
    {
        public ApplicationDbContext dbContext { get; set; }
        private INotificationService notificationService;

        public NotificationServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-BloodDonationApp20")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.notificationService = new NotificationService(dbContext);
        }

        [Fact]
        public void TestAddNotificationShouldAddCorrectly()
        {
            string content = "We need 0+ blood fast!";
            var user = this.AddUser("1", "Test");
            Notification notification = this.notificationService.AddNotification(content, user.Id);

            Assert.NotNull(notification);
        }

        [Fact]
        public void TestAddNotificationShouldNotAddWithEmptyContent()
        {
            string content = "";
            var user = this.AddUser("2", "Test2");
            Notification notification = this.notificationService.AddNotification(content, user.Id);

            Assert.Null(notification);
        }

        [Fact]
        public void TestAddNotificationShouldNotAddWithInvalidUserId()
        {
            string content = "";
            var user = this.AddUser("3", "Test3");
            Notification notification = this.notificationService.AddNotification(content, "1326");

            Assert.Null(notification);
        }

        [Fact]
        public void TestGetAllByUserShouldWorkCorrectly()
        {
            this.notificationService.AddNotification("test", "1");
            List<Notification> notification = this.notificationService.GetAllByUser("1");

            Assert.True(notification.Count() == 2);
        }

        [Fact]
        public void TestGetAllByUserShouldNotGetWithInvalidUserId()
        {
            List<Notification> notification = this.notificationService.GetAllByUser("526");

            Assert.True(notification.Count() == 0);
        }

        [Fact]
        public void TestGetUnreadedCountShouldReturn1()
        {
            this.notificationService.AddNotification("test", "1");
            var result = this.notificationService.GetUnreadedCount("Test");

            Assert.True(result == 0);
        }

        [Fact]
        public void TestReadAllToUserWithExistingUser()
        {
            var result = this.notificationService.ReadAllToUser("1");

            Assert.True(result);
        }

        [Fact]
        public void TestReadAllToUserWithInvalidUser()
        {
            var result = this.notificationService.ReadAllToUser("99");

            Assert.False(result);
        }

        private ApplicationUser AddUser(string id, string username)
        {
            ApplicationUser user = new ApplicationUser
            {
                Id = id,
                UserName = username,
                Email = "test@abv.bg",
                FirstName = "Test",
                LastName = "Testov",
                BloodGroup = "A+",
                PhoneNumber = "0882174569",
                DonatedTimesCount = 0,
                Town = "Smolqn",
                LastTimeDonated = new DateTime(1900, 1, 1)
            };
            this.dbContext.Users.Add(user);
            this.dbContext.SaveChanges();

            return user;
        }
    }
}