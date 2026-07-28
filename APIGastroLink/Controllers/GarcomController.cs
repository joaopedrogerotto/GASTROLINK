using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class GarcomController : ControllerBase {
        private IFacadePedido _facadePedido;

        public GarcomController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        [HttpGet]
        [Authorize("Atendimento")]
        public async Task<IActionResult> PedidosProntosAsync() {
            try {
                var pedidos = await _facadePedido.SelecionaPedidosProntos();
                return Ok(pedidos);
            }catch(Exception ex) {
                return BadRequest(ex.ToString());
            }
        }
    }
}
