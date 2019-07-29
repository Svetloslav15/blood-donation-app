namespace BloodDonationApp.Models.ViewModels
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;

    public class CenterViewModel
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public string Town { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public IList<Request> Requests { get; set; }
    }
}