namespace BloodDonationApp.Models.DbModels
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class Post
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        [Key]
        public string Id { get; set; }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [MinLength(20)]
        public string Description { get; set; }

        public int Likes { get; set; }

        public int DisLikes { get; set; }
    }
}