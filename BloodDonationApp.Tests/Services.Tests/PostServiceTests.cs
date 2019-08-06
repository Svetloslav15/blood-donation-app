namespace BloodDonationApp.Tests.Services.Tests
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using Xunit;

    public class PostServiceTests
    {
        public ApplicationDbContext dbContext { get; set; }
        private IPostService postService;

        public PostServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-BloodDonationApp20")
                .Options;
            this.dbContext = new ApplicationDbContext(options);
            this.postService = new PostService(dbContext);
        }

        [Fact]
        public void TestCreatePostShouldBeAddedCorrectly()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some text",
                Description = "Some description"
            };

            var user = this.AddUser("12", "Iordan");
            Post result = this.postService.Create(model, user.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestCreatePostShouldWorkWithInvalidData()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "",
                Description = ""
            };

            Post result = this.postService.Create(model, "12");

            Assert.Null(result);
        }

        [Fact]
        public void TestCreatePostShouldNotAddWithInvalidUser()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some text",
                Description = "Some description"
            };

            Post result = this.postService.Create(model, "112");

            Assert.Null(result);
        }

        [Fact]
        public void TestGetPostByIdShouldWorkCorrectly()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some more text",
                Description = "Some more description"
            };

            Post post = this.postService.Create(model, "12");
            Post result = this.postService.GetPostById(post.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestGetPostByIdShouldWorkWithInvalidId()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some more text",
                Description = "Some more description"
            };

            Post post = this.postService.Create(model, "112");
            Post result = this.postService.GetPostById("dsffvdv542");

            Assert.Null(result);
        }

        [Fact]
        public void TestDeletePostShouldWorkCorrectly()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some more text",
                Description = "Some more description"
            };

            Post post = this.postService.Create(model, "12");
            Post result = this.postService.DeletePostById(post.Id);

            Assert.NotNull(result);
        }

        [Fact]
        public void TestDeletePostShouldWorkCorrectlyWithInvalidId()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some more text fgdgd",
                Description = "Some more description"
            };

            Post post = this.postService.Create(model, "12");
            Post result = this.postService.DeletePostById("huiszdhvs");

            Assert.Null(result);
        }

        [Fact]
        public void TestEditPostShouldWorkCorrectly()
        {
            PostInputModel model = new PostInputModel()
            {
                Title = "Some more text fgdgd",
                Description = "Some more description"
            };

            Post post = this.postService.Create(model, "12");
            model.Title = "Edited";
            model.Id = post.Id;
            Post result = this.postService.EditPost(model);

            Assert.True(result.Title == "Edited");
        }

        [Fact]
        public void TestGetAllShouldWork()
        {
            var result = this.postService.GetAll();
            Assert.True(result.Count >= 1);
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