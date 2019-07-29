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

        public async Task<IActionResult> Edit(string id)
        {
            var me = await this.userManager.GetUserAsync(HttpContext.User);
            Request request = this.requestService.GetRequestById(id);
            RequestInputModel model = new RequestInputModel
            {
                Id = id,
                BloodGroup = request.BloodGroup,
                Description = request.Description,
                CenterId = request.CenterId,
                AuthorId = me.Id
            };

            return this.View(model);
        }

        [HttpPost]
        public IActionResult Edit(RequestInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.requestService.EditRequest(inputModel.Id, inputModel);
            }
            return this.Redirect("/Center/Details/" + inputModel.CenterId);
        }

        public IActionResult Delete(string centerId, string id)
        {
            this.requestService.DeleteRequest(id);
            return this.Redirect("/Center/Details/" + centerId);
        }

        public async Task<IActionResult> Apply(string centerId, string requestId)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(HttpContext.User);
            this.requestService.ApplyForRequest(currentUser, requestId);

            return this.Redirect("/Center/Details/" + centerId);
        }

        public async Task<IActionResult> UnApply(string centerId, string requestId)
        {
            ApplicationUser currentUser = await this.userManager.GetUserAsync(HttpContext.User);
            this.requestService.UnApplyForRequest(currentUser, requestId);

            return this.Redirect("/Center/Details/" + centerId);
        }
    }
}