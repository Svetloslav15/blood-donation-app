namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using System.Collections.Generic;

    public interface IRequestService
    {
        void AddNewRequestToCenter(RequestInputModel inputModel);
        
        void ApplyForRequest(ApplicationUser currentUser, string requestId);

        void UnApplyForRequest(ApplicationUser currentUser, string requestId);

        void EditRequest(string requestId, RequestInputModel inputModel);

        void DeleteRequest(string id);

        Request GetRequestById(string id);

        void SendEmail(ApplicationUser sender, ApplicationUser receiver, string mailDescription);

        IList<ApplicationUser> Appliers(string requestId);
    }
}