namespace BloodDonationApp.Models.ViewModels
{

    public class ApplierViewModel
    {
        public string Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public string Town { get; set; }

        public string BloodGroup { get; set; }

        public bool CanDonate { get; set; }
    }
}