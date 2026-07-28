using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class GarcomController : Controller{
        private readonly IFacadePedido _facadePedido;

        public GarcomController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        public async Task<IActionResult> PedidosProntos() {
            var pedidos = await _facadePedido.PedidosProntos();
            return View(pedidos);
        }
    }
}
