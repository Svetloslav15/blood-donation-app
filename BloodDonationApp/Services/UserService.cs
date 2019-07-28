namespace BloodDonationApp.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Identity;

    public class UserService : IUserService
    {
        private ApplicationDbContext dbContext;

        public UserService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public ICollection<ApplicationUser> GetAllPotentialDonors(string bloodGroup)
        {
            return this.dbContext.Users
                .Where(x => x.BloodGroup == bloodGroup)
                .ToList();
        }

        public ApplicationUser GetUserById(string id)
        {
            return this.dbContext.Users
                .FirstOrDefault(x => x.Id == id);
        }
    }
}