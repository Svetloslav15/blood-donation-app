namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.ViewModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserController : Controller
    {
        private IUserService userService;
        private ICenterService centerService;
        private UserManager<ApplicationUser> userManager;

        public UserController(IUserService userService, UserManager<ApplicationUser> userManager,
                              ICenterService centerService)
        {
            this.userService = userService;
            this.centerService = centerService;
            this.userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> GiveAdminRights(string userId, string centerName)
        {
            ApplicationUser user = this.userService.GetUserById(userId);
            Center center = this.centerService.GetCenterByName(centerName);
            await this.userManager.AddToRoleAsync(user, "CenterAdmin");
            this.userService.MakeUserAdmin(user.Id, center.Id);
            return this.Redirect("/User/Index");
        }
       
        public async Task<IActionResult> RemoveAdminRights(string id)
        {
            ApplicationUser user = this.userService.GetUserById(id);
            this.userService.RemoveUserAdmin(user.Id);
            await this.userManager.RemoveFromRoleAsync(user, "CenterAdmin");
            return this.Redirect("/User/Index");
        }

        public async Task<IActionResult> Index()
        {
            var users = this.userService.GetAllUsers();
            var result = new List<UserViewModel>();
            foreach (var user in users)
            {
                bool isInRole = await this.userManager.IsInRoleAsync(user, "CenterAdmin");
                UserViewModel model = new UserViewModel
                {
                       Id = user.Id,
                       FirstName = user.FirstName,
                       LastName = user.LastName,
                       Email = user.Email,
                       IsInRole = isInRole
                };
                result.Add(model);
            }
            var centers = this.centerService.GetAllCenters().ToList();
            AllUsersViewModel viewModel = new AllUsersViewModel()
            {
                Users = result,
                Centers = centers
            };
            return View(viewModel);
        }

        public IActionResult Donate(string userId, string requestId)
        {
            this.userService.Donate(userId);
            return this.Redirect("/Request/Appliers?requestId=" + requestId);
        }
    }
}