namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Linq;
    using System.Threading.Tasks;

    public class PostController : Controller
    {
        private IPostService postService;
        private UserManager<ApplicationUser> userManager;

        public PostController(IPostService postService, UserManager<ApplicationUser> userManager)
        {
            this.postService = postService;
            this.userManager = userManager;
        }
        
        public IActionResult Index()
        {
            var posts = this.postService.GetAll().ToList();
            return View(posts);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(PostInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                this.postService.Create(model, user.Id);
            }
            return this.Redirect("/Post/Index");
        }

        public IActionResult Delete(string id)
        {
            this.postService.DeletePostById(id);
            return this.Redirect("/Post/Index");
        }

        public IActionResult Edit(string id)
        {
            var post = this.postService.GetPostById(id);
            PostInputModel postInputModel = new PostInputModel()
            {
                Id = id,
                Title = post.Title,
                Description = post.Description
            };
            return this.View(postInputModel);
        }

        [HttpPost]
        public IActionResult Edit(PostInputModel model)
        {
            if (this.ModelState.IsValid)
            {
                this.postService.EditPost(model);
            }
            return this.Redirect("/Post/Index");
        }
    }
}