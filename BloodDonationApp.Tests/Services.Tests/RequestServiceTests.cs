namespace BloodDonationApp.Tests.Services.Tests
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Moq;
    using Xunit;

    public class RequestServiceTests
    {
        [Fact]
        public void TestCreateRequest()
        {
            RequestInputModel inputModel = new RequestInputModel
            {
                BloodGroup = "A+",
                Description = "Some text",
                AuthorId = "1",
                CenterId = "2"
            };
            var result = new Request()
            {
                Description = inputModel.Description,
                BloodGroup = inputModel.BloodGroup
            };
            var service = new Mock<IRequestService>();
                service.Setup(x => x.AddNewRequestToCenter(inputModel))
                .Returns(true);

            var resultCheck = service.Object.AddNewRequestToCenter(inputModel);
            Assert.True(resultCheck);
        }

        [Fact]
        public void TestCreateRequestWithInvalidData()
        {
            RequestInputModel inputModel = new RequestInputModel
            {
                BloodGroup = "A+",
                Description = "",
                AuthorId = "1",
                CenterId = "2"
            };
            var result = new Request()
            {
                Description = inputModel.Description,
                BloodGroup = inputModel.BloodGroup
            };
            var service = new Mock<IRequestService>();
            service.Setup(x => x.AddNewRequestToCenter(inputModel))
            .Returns(false);

            var resultCheck = service.Object.AddNewRequestToCenter(inputModel);
            Assert.False(resultCheck);
        }

        [Fact]
        public void TestGetRequestById()
        {
            RequestInputModel inputModel = new RequestInputModel
            {
                BloodGroup = "A+",
                Description = "",
                AuthorId = "1",
                CenterId = "2"
            };
            var result = new Request()
            {
                Description = inputModel.Description,
                BloodGroup = inputModel.BloodGroup
            };
            var service = new Mock<IRequestService>();
            service.Setup(x => x.AddNewRequestToCenter(inputModel))
            .Returns(false);

            service.Setup(x => x.GetRequestById("1"))
                .Returns(new Request() { Id = "1" });

            service.Object.AddNewRequestToCenter(inputModel);
            var resultCheck = service.Object.GetRequestById("1");
            Assert.NotNull(resultCheck);
        }

        [Fact]
        public void TestDeleteRequestMethod()
        {
            var service = new Mock<IRequestService>();
            service.Setup(x => x.DeleteRequest("12"))
            .Returns(false);

            var result = service.Object.DeleteRequest("12");
            Assert.False(result);
        }

        [Fact]
        public void TestApplyForRequestRequestMethod()
        {
            var service = new Mock<IRequestService>();
            service.Setup(x => x.ApplyForRequest(new ApplicationUser() { Id = "1" }, "12"))
            .Returns(false);

            var result = service.Object.ApplyForRequest(new ApplicationUser() { Id = "1" }, "12");
            Assert.False(result);
        }

        [Fact]
        public void TestUnApplyForRequestRequestMethod()
        {
            var service = new Mock<IRequestService>();
            service.Setup(x => x.UnApplyForRequest(new ApplicationUser() { Id = "1" }, "12"))
            .Returns(false);

            var result = service.Object.UnApplyForRequest(new ApplicationUser() { Id = "1" }, "12");
            Assert.False(result);
        }
    }
}