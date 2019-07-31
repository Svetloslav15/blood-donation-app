namespace BloodDonationApp.Models.ViewModels
{
    using System.Collections.Generic;
    
    public class AppliersViewModel
    {
        public string RequestId { get; set; }

        public IList<ApplierViewModel> Users { get; set; }
    }
}