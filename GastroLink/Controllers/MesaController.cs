using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class MesaController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }
    }
}
