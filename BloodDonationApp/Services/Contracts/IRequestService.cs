namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;

    public interface IRequestService
    {
        void AddNewRequestToCenter(RequestInputModel inputModel);
        
        void ApplyForRequest(ApplicationUser user);

        void UnApplyForRequest(ApplicationUser user);

        void EditRequest(RequestInputModel inputModel);

        void DeleteRequest(string id);

        void SendEmail(ApplicationUser sender, ApplicationUser receiver, string mailDescription);
    }
}