namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Linq;

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
                Name = inputModel.Name,
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
            var centerToRemove = this.GetCenterById(id);
            this.dbContext.Centers.Remove(centerToRemove);
            this.dbContext.SaveChanges();
        }

        public void EditCenter(CenterInputModel inputModel)
        {
            Center center = this.dbContext.Centers.FirstOrDefault(x => x.Id == inputModel.Id);
            center.Name = inputModel.Name;
            center.PhoneNumber = inputModel.PhoneNumber;
            center.Email = inputModel.Email;
            center.Address = inputModel.Address;
            center.Town = inputModel.Town;

            this.dbContext.SaveChanges();
        }

        public ICollection<Center> GetAllCenters()
        {
            return this.dbContext.Centers.ToList();
        }

        public Center GetCenterById(string id)
        {
            return this.dbContext.Centers
                .Include(x => x.Requests)
                    .ThenInclude(r => r.UserRequests)
                .FirstOrDefault(x => x.Id == id);
        }
    }
}