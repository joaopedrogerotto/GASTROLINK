using GastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class LoginController : Controller {
        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public IActionResult Autenticar(Login Login) {
            return View();
        }
    }
}
