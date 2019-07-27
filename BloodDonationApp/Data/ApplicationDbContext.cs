namespace BloodDonationApp.Data
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.DbModels.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;


    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, IdentityRole, string>
    {
        public DbSet<Center> Centers { get; set; }

        public DbSet<Notification> Notifications { get; set; }

        public DbSet<Post> Posts { get; set; }

        public DbSet<Request> Requests { get; set; }

        public DbSet<UserRequest> UserRequests { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Center>()
                    .HasMany(center => center.Requests)
                    .WithOne(request => request.Center)
                    .HasForeignKey(request => request.CenterId);

            builder.Entity<ApplicationUser>()
               .HasMany(user => user.Notifications)
               .WithOne(notification => notification.User)
               .HasForeignKey(notification => notification.UserId);

            builder.Entity<ApplicationUser>()
              .HasMany(user => user.Posts)
              .WithOne(post => post.Author)
              .HasForeignKey(post => post.AuthorId);

            builder.Entity<UserRequest>()
                .HasKey(ur => new
                {
                    ur.UserId,
                    ur.RequestId
                });

            builder.Entity<UserRequest>()
              .HasOne(ur => ur.User)
              .WithMany(u => u.UserRequests)
              .HasForeignKey(u => u.UserId);

            builder.Entity<UserRequest>()
              .HasOne(ur => ur.Request)
              .WithMany(u => u.UserRequests)
              .HasForeignKey(u => u.RequestId);

        }
    }
}