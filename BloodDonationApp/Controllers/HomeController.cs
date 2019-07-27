namespace BloodDonationApp.Controllers
{
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using BloodDonationApp.Models;
    using Microsoft.AspNetCore.Identity;
    using BloodDonationApp.Models.DbModels;

    public class HomeController : Controller
    {
        private UserManager<ApplicationUser> userManager;
        public HomeController(UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            if (this.User.Identity.IsAuthenticated)
            {
                var user = await this.userManager.GetUserAsync(HttpContext.User);
                string name = user.FirstName;
                ViewData["Name"] = name;
                return View();
            }
            return this.Redirect("/Home/NotLogged");
        }

        public IActionResult NotLogged()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}