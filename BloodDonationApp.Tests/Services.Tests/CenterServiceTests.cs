namespace BloodDonationApp.Tests
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using Xunit;

    public class CenterServiceTests
    {
        public ApplicationDbContext dbContext { get; set; }

        public CenterServiceTests()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "aspnet-BloodDonationApp20")
                .Options;

            this.dbContext = new ApplicationDbContext(options);
        }

        [Fact]
        public void TestCreateCenterMethodShouldAddToDbCorrectly()
        {
            CenterInputModel centerModel = this.GetInputModel();

            ICenterService centerService = new CenterService(this.dbContext);
            Center result = centerService.CreateCenter(centerModel);
            int entitiesCount = this.dbContext.Centers.Count();

            Assert.NotNull(result);
            //Assert.True(entitiesCount == 1, "Center was not added!");
        }

        [Fact]
        public void TestCreateCenterWithInvalidData()
        {
            CenterInputModel centerModel = new CenterInputModel()
            {
                Name = "SecondCenter",
                Town = "",
                Address = "bul. Vasil Levski 30",
                PhoneNumber = "08742457727",
                Email = "center2gmail.com"
            };

            Center result = null;
            int entitiesCount = 0;
            
            ICenterService centerService = new CenterService(this.dbContext);
            result = centerService.CreateCenter(centerModel);
            entitiesCount = this.dbContext.Centers.Count();
            
            Assert.Null(result);
            Assert.True(entitiesCount == 0, "Center was added but it should not!");
        }

        [Fact]
        public void TestGetCenterByIdShouldWorkCorrectly()
        {
            CenterInputModel centerModel = this.GetInputModel();

            ICenterService centerService = new CenterService(this.dbContext);
            Center currentlyAdded = centerService.CreateCenter(centerModel);
            Center result = centerService.GetCenterById(currentlyAdded.Id);
            Center incorrectResult = centerService.GetCenterById(currentlyAdded.Id + "5");

            Assert.NotNull(result);
            Assert.Null(incorrectResult);
        }

        [Fact]
        public void TestDeleteCenterById()
        {
            CenterInputModel centerModel = this.GetInputModel();
            
            ICenterService centerService = new CenterService(this.dbContext);
            Center currentlyAdded = centerService.CreateCenter(centerModel);
            Center result = centerService.DeleteCenterById(currentlyAdded.Id);
            
            Assert.NotNull(result);
        }

        [Fact]
        public void TestDeleteCenterByIncorrectId()
        {
            CenterInputModel centerModel = this.GetInputModel();

            ICenterService centerService = new CenterService(this.dbContext);
            Center currentlyAdded = centerService.CreateCenter(centerModel);
            Center result = centerService.DeleteCenterById("invalid-id-55595");

            Assert.Null(result);
        }

        [Fact]
        public void TestGetCenterByNameShouldWorkCorrectlyWithBothData()
        {
            CenterInputModel centerModel = this.GetInputModel();

            ICenterService centerService = new CenterService(this.dbContext);
            Center currentlyAdded = centerService.CreateCenter(centerModel);
            Center result = centerService.GetCenterByName("SecondCenter");
            Center resultShouldBeNull = centerService.GetCenterByName("SecondCenter1");

            Assert.NotNull(result);
            Assert.Null(resultShouldBeNull);
        }

        [Fact]
        public void TestGetAllCenters()
        {
            CenterInputModel centerModel = this.GetInputModel();
            CenterInputModel centerTwoModel = this.GetInputModel();
            centerTwoModel.Name = "New";

            ICenterService centerService = new CenterService(this.dbContext);
            var result = centerService.GetAllCenters().Count();
            Assert.True(result == 0, "Centers count is incorrect!");

            centerService.CreateCenter(centerModel);
            centerService.CreateCenter(centerTwoModel);
            result = centerService.GetAllCenters().Count();

            Assert.True(result == 2, "Centers count is incorrect!");
        }

        private CenterInputModel GetInputModel()
        {
            return new CenterInputModel()
            {
                Name = "SecondCenter",
                Town = "Sofia",
                Address = "bul. Vasil Levski 30",
                PhoneNumber = "0874245727",
                Email = "center2@gmail.com"
            };
        }
    }
}