namespace BloodDonationApp.Models.DbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Notification
    {
        public Notification()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string Content { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}