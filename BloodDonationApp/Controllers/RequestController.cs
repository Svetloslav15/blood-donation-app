namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;

    public class RequestController : Controller
    {
        private IRequestService requestService;
        private UserManager<ApplicationUser> userManager;

        public RequestController(IRequestService requestService, UserManager<ApplicationUser> userManager)
        {
            this.requestService = requestService;
            this.userManager = userManager;
        }

        public async Task<IActionResult> Create(string id)
        {
            var me = await this.userManager.GetUserAsync(HttpContext.User);
            ViewData["Id"] = id;
            ViewData["MyId"] = me.Id;
            return View();
        }

        [HttpPost]
        public IActionResult Create(RequestInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.requestService.AddNewRequestToCenter(inputModel);
            }
            return this.Redirect("/");
        }
    }
}