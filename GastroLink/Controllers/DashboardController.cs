using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class DashboardController : Controller{
        public IActionResult Index() {
            return View();
        }
    }
}
