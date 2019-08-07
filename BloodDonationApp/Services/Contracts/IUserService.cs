namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;

    public interface IUserService
    {
        ICollection<ApplicationUser> GetAllPotentialDonors(string bloodGroup);

        ApplicationUser GetUserById(string id);

        ICollection<ApplicationUser> GetAllUsers();

        ApplicationUser MakeUserAdmin(string userId, string centerId);

        ApplicationUser RemoveUserAdmin(string userId);

        bool Donate(string userId);
    }
}