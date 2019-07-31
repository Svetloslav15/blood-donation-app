namespace BloodDonationApp.Services
{
    using BloodDonationApp.Data;
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using System.Collections.Generic;
    using System.Linq;

    public class PostService : IPostService
    {
        private ApplicationDbContext dbContext;
        public PostService(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public void Create(PostInputModel model, string userId)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.Description = model.Description;
            post.AuthorId = userId;

            this.dbContext.Posts.Add(post);
            this.dbContext.SaveChanges();
        }

        public void DeletePostById(string id)
        {
            Post post = this.dbContext.Posts.FirstOrDefault(x => x.Id == id);
            this.dbContext.Posts.Remove(post);
            this.dbContext.SaveChanges();
        }

        public void EditPost(PostInputModel model)
        {
            Post post = this.dbContext.Posts.FirstOrDefault(x => x.Id == model.Id);
            post.Title = model.Title;
            post.Description = model.Description;

            this.dbContext.SaveChanges();
        }

        public ICollection<Post> GetAll()
        {
            return this.dbContext.Posts.ToList();
        }

        public Post GetPostById(string id)
        {
            return this.dbContext.Posts.FirstOrDefault(x => x.Id == id);
        }
    }
}