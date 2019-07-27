namespace BloodDonationApp.Models.DbModels.Contracts
{
    using System.Collections.Generic;

    public interface IRequest
    {
        string Id { get; set; }

        string Description { get; set; }

        string CenterId { get; set; }

        Center Center { get; set; }

        string BloodGroup { get; set; }

        ICollection<UserRequest> UserRequests { get; set; }
    }
}