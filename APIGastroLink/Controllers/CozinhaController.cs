using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "Cozinha")]
    public class CozinhaController : ControllerBase {
        private IFacadePedido _facadePedido;

        public CozinhaController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        [HttpGet("pedidos")]
        public async Task<IActionResult> GetTodosPedidos() {
            try {
                return Ok(await _facadePedido.SelecionarPedidosCozinha());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
