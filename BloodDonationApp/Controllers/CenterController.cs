namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.InputModels.Contracts;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;

    public class CenterController : Controller
    {
        private ICenterService centerService;

        public CenterController(ICenterService centerService)
        {
            this.centerService = centerService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public IActionResult Create()
        {
            return this.View();   
        }

        [HttpPost]
        public IActionResult Create(CenterInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.centerService.CreateCenter(inputModel);
            }
            return this.Redirect("/");
        }
    }
}