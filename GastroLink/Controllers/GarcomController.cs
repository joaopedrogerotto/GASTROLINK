using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class GarcomController : Controller{
        public IActionResult PedidosProntos() {
            return View();
        }
    }
}
