using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PedidoController : ControllerBase {
        private readonly IFacadePedido _facadePedido;

        public PedidoController(IFacadePedido facadePedido) {
            _facadePedido = facadePedido;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoCreateDTO pedido) {
            try {
                await _facadePedido.CadastrarPedido(pedido);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok();
        }
    }
}
