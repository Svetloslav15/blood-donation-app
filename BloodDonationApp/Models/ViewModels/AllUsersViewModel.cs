namespace BloodDonationApp.Models.ViewModels
{
    using BloodDonationApp.Models.DbModels;
    using System.Collections.Generic;
    
    public class AllUsersViewModel
    {
        public IList<UserViewModel> Users { get; set; }

        public IList<Center> Centers { get; set; }
    }
}