namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.InputModels.Contracts;
    using BloodDonationApp.Services.Contracts;
    using System;
    using System.Collections.Generic;

    public class CenterService : ICenterService
    {
        private ApplicationDbContext dbContext;

        public CenterService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        public void CreateCenter(CenterInputModel inputModel)
        {
            Center center = new Center()
            {
                Town = inputModel.Town,
                Address = inputModel.Address,
                PhoneNumber = inputModel.PhoneNumber,
                Email = inputModel.Email
            };
            this.dbContext.Centers.Add(center);
            this.dbContext.SaveChanges();
        }

        public void DeleteCenterById(string id)
        {
            throw new NotImplementedException();
        }

        public void EditCenter(ICenterInputModel inputModel)
        {
            throw new NotImplementedException();
        }

        public ICollection<Center> GetAllCenters()
        {
            throw new NotImplementedException();
        }

        public Center GetCenterById(string id)
        {
            throw new NotImplementedException();
        }
    }
}
