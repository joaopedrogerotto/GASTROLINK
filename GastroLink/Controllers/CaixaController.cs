using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class CaixaController : Controller {
        private readonly IFacadePedido _facadePedido;

        public CaixaController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        public async Task<IActionResult> TodosPedidos() {
            var pedidos = await _facadePedido.PedidosCaixa();
            return View(pedidos);
        }
    }
}
