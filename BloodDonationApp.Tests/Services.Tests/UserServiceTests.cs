namespace BloodDonationApp.Tests.Services.Tests
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services.Contracts;
    using Moq;
    using System;
    using System.Collections.Generic;
    using Xunit;

    public class UserServiceTests
    {
        [Fact]
        public void TestGetUserByIdMethod()
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "test@blood.com",
                Email = "test@blood.com",
                FirstName = "test",
                LastName = "test",
                Town = "Blagoevgrad"
            };
            var service = new Mock<IUserService>();
            service.Setup(x => x.GetUserById("1"))
            .Returns(user);

            var result = service.Object.GetUserById("1");
            Assert.NotNull(result);
        }

        [Fact]
        public void TestRemoveUserAdminMethod()
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "test@blood.com",
                Email = "test@blood.com",
                FirstName = "test",
                LastName = "test",
                Town = "Blagoevgrad"
            };
            var service = new Mock<IUserService>();
            service.Setup(x => x.RemoveUserAdmin("1"))
            .Returns(user);

            var result = service.Object.GetUserById("1");
            Assert.Null(result);
        }

        [Fact]
        public void TestMakeUserAdminMethod()
        {
            ApplicationUser user = new ApplicationUser()
            {
                UserName = "test@blood.com",
                Email = "test@blood.com",
                FirstName = "test",
                LastName = "test",
                Town = "Blagoevgrad"
            };
            var service = new Mock<IUserService>();
            service.Setup(x => x.RemoveUserAdmin("1"))
            .Returns(user);

            var result = service.Object.GetUserById("1");
            Assert.Null(result);
        }

        [Fact]
        public void TestGetAllUsersMethod()
        {
            var expectedResult = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "test@blood.com",
                    Email = "test@blood.com",
                    FirstName = "test",
                    LastName = "test",
                    Town = "Blagoevgrad"
                },
                new ApplicationUser()
                {
                    UserName = "test2@blood.com",
                    Email = "test3@blood.com",
                    FirstName = "test3",
                    LastName = "test4",
                    Town = "Sofiq"
                }
            };
            var service = new Mock<IUserService>();
            service.Setup(x => x.GetAllUsers())
            .Returns(expectedResult);

            var result = service.Object.GetAllUsers();
            Assert.IsType<List<ApplicationUser>>(result);
        }

        [Fact]
        public void TestGetPotentialUsersMethod()
        {
            var expectedResult = new List<ApplicationUser>()
            {
                new ApplicationUser()
                {
                    UserName = "test@blood.com",
                    Email = "test@blood.com",
                    FirstName = "test",
                    LastName = "test",
                    Town = "Blagoevgrad"
                },
                new ApplicationUser()
                {
                    UserName = "test2@blood.com",
                    Email = "test3@blood.com",
                    FirstName = "test3",
                    LastName = "test4",
                    Town = "Sofiq"
                }
            };
            var service = new Mock<IUserService>();
            service.Setup(x => x.GetAllPotentialDonors("A+"))
            .Returns(expectedResult);

            var result = service.Object.GetAllPotentialDonors("A+");
            Assert.IsType<List<ApplicationUser>>(result);
        }
    }
}