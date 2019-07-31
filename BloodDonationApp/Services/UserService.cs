namespace BloodDonationApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private ApplicationDbContext dbContext;
        private UserManager<ApplicationUser> userManager;

        public UserService(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager)
        {
            this.dbContext = dbContext;
            this.userManager = userManager;
        }

        public void Donate(string userId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            user.LastTimeDonated = DateTime.Now;
            user.DonatedTimesCount++;

            this.dbContext.SaveChanges();
        }

        public ICollection<ApplicationUser> GetAllPotentialDonors(string bloodGroup)
        {
            var possibleGroups = this.GetPossibleBloodGroups(bloodGroup);

            return this.dbContext.Users
                .Where(x => possibleGroups.Contains(x.BloodGroup) && 
                    (Math.Abs(((x.LastTimeDonated.Year - DateTime.Now.Year) * 12) + x.LastTimeDonated.Month - DateTime.Now.Month) >= 3))
                .ToList();
        }

        public ICollection<ApplicationUser> GetAllUsers()
        {
            return this.dbContext.Users.ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return this.dbContext.Users.FirstOrDefault(x => x.Id == id);
        }

        public void MakeUserAdmin(string userId, string centerId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            user.AdminCenterId = centerId;
            this.dbContext.SaveChanges();
        }

        public void RemoveUserAdmin(string userId)
        {
            var user = this.dbContext.Users.FirstOrDefault(x => x.Id == userId);
            user.AdminCenterId = null;
            this.dbContext.SaveChanges();
        }

        private List<string> GetPossibleBloodGroups(string bloodGroup)
        {
            List<string> bloodGroups = new List<string>();
            if (bloodGroup == "0+")
            {
                bloodGroups.Add("0+");
                bloodGroups.Add("A+");
                bloodGroups.Add("B+");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "A+")
            {
                bloodGroups.Add("A+");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "B+")
            {
                bloodGroups.Add("B+");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "AB+")
            {
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "A-")
            {
                bloodGroups.Add("A-");
                bloodGroups.Add("A+");
                bloodGroups.Add("AB-");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "0-")
            {
                bloodGroups.Add("0-");
                bloodGroups.Add("0+");
                bloodGroups.Add("B+");
                bloodGroups.Add("B-");
                bloodGroups.Add("A-");
                bloodGroups.Add("A+");
                bloodGroups.Add("AB-");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "B-")
            {
                bloodGroups.Add("B+");
                bloodGroups.Add("B-");
                bloodGroups.Add("AB-");
                bloodGroups.Add("AB+");
            }
            else if (bloodGroup == "AB-")
            {
                bloodGroups.Add("AB-");
                bloodGroups.Add("AB+");
            }
            return bloodGroups;
        }
    }
}