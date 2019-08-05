namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using System.Collections.Generic;

    public interface ICenterService
    {
        Center CreateCenter(CenterInputModel inputModel);

        ICollection<Center> GetAllCenters();

        Center EditCenter(CenterInputModel inputModel);

        Center GetCenterById(string id);

        Center DeleteCenterById(string id);

        Center GetCenterByName(string name);
    }
}