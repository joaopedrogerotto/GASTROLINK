using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class CozinhaController : Controller{

        public IActionResult TodosPedidos() {
            return View();
        }

    }
}
