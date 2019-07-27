namespace BloodDonationApp.Controllers
{
    using BloodDonationApp.Models.DbModels;
    using BloodDonationApp.Models.InputModels;
    using BloodDonationApp.Models.ViewModels;
    using BloodDonationApp.Services.Contracts;
    using Microsoft.AspNetCore.Mvc;
    using System.Collections.Generic;
    using System.Linq;

    public class CenterController : Controller
    {
        private ICenterService centerService;

        public CenterController(ICenterService centerService)
        {
            this.centerService = centerService;
        }

        public IActionResult Index()
        {
            ICollection<Center> centers = this.centerService.GetAllCenters();
            return View(new AllCentersViewModel() { Centers = centers});
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

        public IActionResult Edit(string id)
        {
            Center center = this.centerService.GetCenterById(id);
            CenterInputModel centerInputModel = new CenterInputModel()
            {
                Id = center.Id,
                Name = center.Name,
                Address = center.Address,
                PhoneNumber = center.PhoneNumber,
                Email = center.Email,
                Town = center.Town,
            };

            return this.View(centerInputModel);
        }

        [HttpPost]
        public IActionResult Edit(CenterInputModel inputModel)
        {
            if (this.ModelState.IsValid)
            {
                this.centerService.EditCenter(inputModel);
            }
            return this.Redirect("/");
        }

        public IActionResult Delete(string id)
        {
            this.centerService.DeleteCenterById(id);
            return this.Redirect("/Center/Index");
        }
        
        public IActionResult Details(string id)
        {
            Center center = this.centerService.GetCenterById(id);
            CenterInputModel centerInputModel = new CenterInputModel()
            {
                Id = center.Id,
                Name = center.Name,
                Address = center.Address,
                PhoneNumber = center.PhoneNumber,
                Email = center.Email,
                Town = center.Town,
            };

            return this.View(centerInputModel);
        }
    }
}