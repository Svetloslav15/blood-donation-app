namespace BloodDonationApp.Models.DbModels.Contracts
{
    public interface IUserRequest
    {
        string UserId { get; set; }
        ApplicationUser User { get; set; }

        string RequestId { get; set; }
        IRequest Request { get; set; }
    }
}