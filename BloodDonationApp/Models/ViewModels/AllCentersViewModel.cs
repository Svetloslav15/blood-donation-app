namespace BloodDonationApp.Models.ViewModels
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;

    public class AllCentersViewModel
    {
        public ICollection<Center> Centers { get; set; }
    }
}
