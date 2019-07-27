namespace BloodDonationApp.Models.InputModels.Contracts
{
    public interface ICenterInputModel
    {
        string Town { get; set; }

        string Address { get; set; }

        string PhoneNumber { get; set; }

        string Email { get; set; }
    }
}