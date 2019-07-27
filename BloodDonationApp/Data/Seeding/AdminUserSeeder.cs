namespace BloodDonationApp.Data.Seeding
{
    using BloodDonationApp.Models.DbModels;
    using Microsoft.AspNetCore.Identity;
    using System.Threading.Tasks;

    public class AdminUserSeeder : ISeeder
    {
        private readonly UserManager<ApplicationUser> userManager;

        public AdminUserSeeder(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task Seed()
        {
            var user = new ApplicationUser
            {
                UserName = "Admin",
                Email = "admin@blood.com",
                FirstName = "Admin",
                LastName = "Admin",
            };

            var result = await this.userManager.CreateAsync(user, "systemadmin1");
        }
    }
}