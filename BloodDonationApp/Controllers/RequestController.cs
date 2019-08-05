namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.ViewModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
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

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
        public async Task<IActionResult> Create(string id)
        {
            var me = await this.userManager.GetUserAsync(HttpContext.User);
            ViewData["Id"] = id;
            ViewData["MyId"] = me.Id;
            return View();
        }

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
        [HttpPost]
        public IActionResult Create(RequestInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.requestService.AddNewRequestToCenter(inputModel);
            }
            return this.Redirect("/");
        }

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
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

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
        [HttpPost]
        public IActionResult Edit(RequestInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.requestService.EditRequest(inputModel.Id, inputModel);
            }
            return this.Redirect("/Center/Details/" + inputModel.CenterId);
        }

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
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

        [Authorize(Roles = "SystemAdmin,CenterAdmin")]
        public IActionResult Appliers(string requestId)
        {
            var users = this.requestService.Appliers(requestId);
            var usersModels = users.Select(x => new ApplierViewModel()
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                PhoneNumber = x.PhoneNumber,
                Email = x.Email,
                Town = x.Town,
                BloodGroup = x.BloodGroup,
                CanDonate = Math.Abs(((x.LastTimeDonated.Year - DateTime.Now.Year) * 12) + x.LastTimeDonated.Month - DateTime.Now.Month) >= 3
            }).ToList();
            AppliersViewModel viewModel = new AppliersViewModel()
            {
                Users = usersModels,
                RequestId = requestId
            };
            return this.View(viewModel);
        }
    }
}