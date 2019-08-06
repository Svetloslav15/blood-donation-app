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

        public Post Create(PostInputModel model, string userId)
        {
            ApplicationUser user = this.dbContext.Users
                .FirstOrDefault(x => x.Id == userId);
            Post post = null;
            if (model.Title.Trim() != "" && model.Description.Trim() != "" &&
                user != null)
            {
                post = new Post();
                post.Title = model.Title;
                post.Description = model.Description;
                post.AuthorId = userId;

                this.dbContext.Posts.Add(post);
                this.dbContext.SaveChanges();
            }
            return post;
        }

        public Post DeletePostById(string id)
        {
            Post post = this.dbContext.Posts.FirstOrDefault(x => x.Id == id);
            if (post != null)
            {
                this.dbContext.Posts.Remove(post);
                this.dbContext.SaveChanges();
            }
            return post;
        }

        public Post EditPost(PostInputModel model)
        {
            Post post = this.dbContext.Posts.FirstOrDefault(x => x.Id == model.Id);
            post.Title = model.Title;
            post.Description = model.Description;

            this.dbContext.SaveChanges();
            return post;
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