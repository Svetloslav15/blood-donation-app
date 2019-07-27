namespace BloodDonationApp.Models.DbModels.Contracts
{
    using System;
    using System.Collections.Generic;

    public interface IPost
    {
        string Id { get; set; }

        string AuthorId { get; set; }

        ApplicationUser Author { get; set; }

        string Title { get; set; }

        string Description { get; set; }

        int Likes { get; set; }

        int DisLikes { get; set; }
    }
}