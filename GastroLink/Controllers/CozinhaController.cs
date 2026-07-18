using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class CozinhaController : Controller {
        private readonly IFacadePedido _facadePedido;

        public CozinhaController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        public async Task<IActionResult> TodosPedidos() {
            try {
                return View(await _facadePedido.PedidosCozinhaPendente());
            } catch (InvalidOperationException iEx) {
                throw new Exception(iEx.Message);
            }
        }

    }
}
