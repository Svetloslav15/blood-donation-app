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
        public Center CreateCenter(CenterInputModel inputModel)
        {
            Center center = null;
            if (inputModel.Name.Trim() != "" && inputModel.Town.Trim() != "" &&
                inputModel.Address.Trim() != "" && inputModel.PhoneNumber.Trim() != "" &&
                inputModel.Email.Trim() != "")
            {
                center = new Center()
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

            return center;
        }

        public Center DeleteCenterById(string id)
        {
            Center centerToRemove = this.GetCenterById(id);
            if (centerToRemove != null)
            {
                this.dbContext.Centers.Remove(centerToRemove);
                this.dbContext.SaveChanges();
            }

            return centerToRemove;
        }

        public Center EditCenter(CenterInputModel inputModel)
        {
            Center center = this.dbContext.Centers.FirstOrDefault(x => x.Id == inputModel.Id);
            center.Name = inputModel.Name;
            center.PhoneNumber = inputModel.PhoneNumber;
            center.Email = inputModel.Email;
            center.Address = inputModel.Address;
            center.Town = inputModel.Town;

            this.dbContext.SaveChanges();

            return center;
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

        public Center GetCenterByName(string name)
        {
            return this.dbContext.Centers.FirstOrDefault(x => x.Name == name);
        }
    }
}