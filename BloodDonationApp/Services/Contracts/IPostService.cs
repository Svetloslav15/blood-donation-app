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

        void Create(PostInputModel model, string authorId);

        void EditPost(PostInputModel model);

        void DeletePostById(string id);

    }
}