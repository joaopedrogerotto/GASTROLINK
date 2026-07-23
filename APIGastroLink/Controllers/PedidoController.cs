using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PedidoController : ControllerBase {
        private readonly IFacadePedido _facadePedido;
        private readonly IPedidoNotificacaoService _pedidoNotificacaoService;

        public PedidoController(IFacadePedido facadePedido, IPedidoNotificacaoService pedidoNotificacaoService) {
            _facadePedido = facadePedido;
            _pedidoNotificacaoService = pedidoNotificacaoService;
        }

        [HttpPost]
        [Authorize("Atendimento")]
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoCreateDTO pedido) {
            try {
                var novoPedido = await _facadePedido.CadastrarPedido(pedido);
                await _pedidoNotificacaoService.NovoPedido(novoPedido);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        [Authorize]
        public async Task<IActionResult> AtualizarStatusPedido([FromBody] StatusPedidoUpdateDTO StatusPedidoUpdateDTO) {
            if (StatusPedidoUpdateDTO.IdPedido == 0 || StatusPedidoUpdateDTO.IdStatusPedido == 0) {
                return BadRequest();
            }

            try {
                await _facadePedido.AtualizarStatus(StatusPedidoUpdateDTO);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
