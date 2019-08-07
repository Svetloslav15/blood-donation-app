namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using System.Collections.Generic;

    public interface IRequestService
    {
        bool AddNewRequestToCenter(RequestInputModel inputModel);
        
        bool ApplyForRequest(ApplicationUser currentUser, string requestId);

        bool UnApplyForRequest(ApplicationUser currentUser, string requestId);

        bool EditRequest(string requestId, RequestInputModel inputModel);

        bool DeleteRequest(string id);

        Request GetRequestById(string id);

        void SendEmail(ApplicationUser sender, ApplicationUser receiver, string mailDescription);

        IList<ApplicationUser> Appliers(string requestId);
    }
}