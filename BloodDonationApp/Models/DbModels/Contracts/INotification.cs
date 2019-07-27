namespace BloodDonationApp.Models.DbModels.Contracts
{
    public interface INotification
    {
        string Id { get; set; }

        string Content { get; set; }

        string UserId { get; set; }

        ApplicationUser User { get; set; }
    }
}