using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class PedidoController : Controller {
        private readonly IFacadeMesa _facadeMesa;

        public PedidoController(IFacadeMesa facadeMesa) {
            _facadeMesa = facadeMesa;
        }

        public async Task<IActionResult> Index() {
            return View();
        }
    }
}
