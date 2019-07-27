namespace BloodDonationApp.Models.DbModels.Contracts
{
    using System.Collections.Generic;

    public interface ICenter
    {
        string Id { get; set; }

        string Town { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }

        string Email { get; set; }

        ICollection<IRequest> Requests { get; set; }
    }
}