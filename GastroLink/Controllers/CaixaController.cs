using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    [Authorize(Policy = "Caixa")]
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
