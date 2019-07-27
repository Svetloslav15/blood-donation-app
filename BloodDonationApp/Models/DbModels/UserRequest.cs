namespace BloodDonationApp.Models.DbModels
{
    public class UserRequest
    {
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public string RequestId { get; set; }
        public Request Request { get; set; }
    }
}