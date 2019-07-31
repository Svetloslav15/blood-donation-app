namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;

    public interface IUserService
    {
        ICollection<ApplicationUser> GetAllPotentialDonors(string bloodGroup);

        ApplicationUser GetUserById(string id);

        ICollection<ApplicationUser> GetAllUsers();

        void MakeUserAdmin(string userId, string centerId);

        void RemoveUserAdmin(string userId);

        void Donate(string userId);
    }
}