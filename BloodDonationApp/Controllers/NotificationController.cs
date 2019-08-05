namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class NotificationController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        private INotificationService notificationService;

        public NotificationController(INotificationService notificationService, UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.notificationService = notificationService;
        }

        public async Task<IActionResult> GetAll()
        {
            var user = await this.userManager.GetUserAsync(HttpContext.User);
            var notifications = this.notificationService.GetAllByUser(user.Id);
            this.notificationService.ReadAllToUser(user.Id);
            return View(notifications);
        }
    }
}