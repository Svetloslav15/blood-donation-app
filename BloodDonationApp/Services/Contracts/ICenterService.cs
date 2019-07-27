namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.InputModels.Contracts;
    using System.Collections.Generic;

    public interface ICenterService
    {
        void CreateCenter(CenterInputModel inputModel);

        ICollection<Center> GetAllCenters();

        void EditCenter(ICenterInputModel inputModel);

        Center GetCenterById(string id);

        void DeleteCenterById(string id);
        
    }
}