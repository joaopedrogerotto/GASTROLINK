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
        [Authorize(Policy = "CriarPedido")]
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

                var pedido = await _facadePedido.SelecionarPeloId(StatusPedidoUpdateDTO.IdPedido);

                if (StatusPedidoUpdateDTO.IdStatusPedido == 4) {
                    await _pedidoNotificacaoService.PedidoPronto(pedido);
                }else if(StatusPedidoUpdateDTO.IdStatusPedido == 6) {
                    await _pedidoNotificacaoService.AguardandoPagamento(pedido);
                }
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idPedido}")]
        [Authorize]
        public async Task<IActionResult> SelecionarPedidoPorId(int idPedido) {
            try {
                var pedido = await _facadePedido.SelecionarPeloId(idPedido);
                return Ok(pedido);
            } catch (Exception ex) {
                return BadRequest(ex.ToString());
            }
        } 
    }
}
