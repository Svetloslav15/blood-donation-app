namespace BloodDonationApp.Data.Seeding
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class AAUserRolesSeeder : ISeeder
    {
        private readonly ApplicationDbContext context;

        public AAUserRolesSeeder(ApplicationDbContext context)
        {
            this.context = context;
        }

        public async Task Seed()
        {
            if (!context.Roles.Any())
            {
                context.Roles.Add(new IdentityRole
                {
                    Name = "SystemAdmin",
                    NormalizedName = "SYSTEMADMIN"
                });

                context.Roles.Add(new IdentityRole
                {
                    Name = "CenterAdmin",
                    NormalizedName = "CENTERADMIN"
                });
            }

            context.SaveChanges();
        }
    }
}