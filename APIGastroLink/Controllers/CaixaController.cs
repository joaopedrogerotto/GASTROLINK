using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class CaixaController : ControllerBase {
        private readonly IFacadePedido _facadePedido;

        public CaixaController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido; 
        }

        [HttpGet]
        [Authorize("Caixa")]
        public async Task<IActionResult> TodosPedidosAsync() {
            try {
                var pedidos = await _facadePedido.SelecionaPedidosCaixa();
                return Ok(pedidos);
            }catch(Exception ex) {
                return BadRequest(ex.ToString());
            }
        }
    }
}
