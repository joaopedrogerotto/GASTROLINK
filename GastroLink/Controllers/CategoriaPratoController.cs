using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class CategoriaPratoController : Controller {
        public IActionResult Cadastrar() {
            return View();
        }
    }
}
