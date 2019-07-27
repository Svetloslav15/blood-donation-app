namespace BloodDonationApp.Models.DbModels
{
    using BloodDonationApp.Models.DbModels.Contracts;
    using Microsoft.AspNetCore.Identity;
    using System;
    using System.Collections.Generic;

    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Town { get; set; }

        public string BloodGroup { get; set; }

        public int DonatedTimesCount { get; set; }

        public DateTime LastTimeDonated { get; set; }

        public ICollection<Notification> Notifications { get; set; }

        public ICollection<UserRequest> UserRequests { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}