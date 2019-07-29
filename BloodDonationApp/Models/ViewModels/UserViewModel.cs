﻿namespace BloodDonationApp.Models.ViewModels
{
    public class UserViewModel
    {
        public string Id { get; set; }
        
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public bool IsInRole { get; set; }
    }
}
