namespace BloodDonationApp.Services.Contracts
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.ViewModels;
    using System.Collections.Generic;

    public interface IPostService
    {
        ICollection<Post> GetAll();

        Post GetPostById(string id);

        Post Create(PostInputModel model, string authorId);

        Post EditPost(PostInputModel model);

        Post DeletePostById(string id);

    }
}