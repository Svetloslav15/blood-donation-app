namespace BloodDonationApp.Models.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Request
    {
        public Request()
        {
            this.Id = Guid.NewGuid().ToString();
            this.UserRequests = new HashSet<UserRequest>();
        }
        
        [Key]
        public string Id { get; set; }

        public string Description { get; set; }

        public string CenterId { get; set; }

        public Center Center { get; set; }

        public string BloodGroup { get; set; }

        public ICollection<UserRequest> UserRequests { get; set; }
    }
}