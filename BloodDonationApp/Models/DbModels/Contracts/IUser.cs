namespace BloodDonationApp.Models.DbModels.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IUser
    {
        string FirstName { get; set; }

        string LastName { get; set; }

        string Town { get; set; }

        string BloodGroup { get; set; }

        string PhoneNumber { get; set; }

        int DonatedTimesCount { get; set; }

        DateTime LastTimeDonated { get; set; }

        ICollection<INotification> Notifications { get; set; }

        ICollection<UserRequest> UserRequests { get; set; }

        ICollection<IPost> Posts { get; set; }
    }
}
