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
                UserName = "admin@blood.com",
                Email = "admin@blood.com",
                FirstName = "SystemAdmin",
                LastName = "SystemAdmin",
                Town = "Blagoevgrad"
            };

            var result = await this.userManager.CreateAsync(user, "SystemAdmin123+");
            var secResult = await this.userManager.AddToRoleAsync(user, "SystemAdmin");

            if (secResult.Succeeded)
            {
                System.Console.WriteLine(5);
            }
            else
            {
                System.Console.WriteLine(7);
            }
        }
    }
}