using APIGastroLink.DTO;
using APIGastroLink.Enums;
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
        private readonly IFacadeAuditoria _facadeAuditoria;

        public PedidoController(IFacadePedido facadePedido, IPedidoNotificacaoService pedidoNotificacaoService, IFacadeAuditoria facadeAuditoria) {
            _facadePedido = facadePedido;
            _pedidoNotificacaoService = pedidoNotificacaoService;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        [Authorize(Policy = "AtendimentoComChatbot")]
        public async Task<IActionResult> CadastrarPedido([FromBody] PedidoCreateDTO pedido) {
            try {
                var novoPedido = await _facadePedido.CadastrarPedido(pedido);
                await _pedidoNotificacaoService.NovoPedido(novoPedido);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Pedido criado pelo garçom {novoPedido.Usuario.Nome}, para a mesa {novoPedido.Mesa.Numero} e enviado para a cozinha", User);
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
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Pedido {pedido.Id} pronto para retirada do garçom, para a mesa {pedido.Mesa.Numero} e enviado para a cozinha", User);
                } else if(StatusPedidoUpdateDTO.IdStatusPedido == 6) {
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Pedido {pedido.Id} entregue e agurdando o pagamento, para a mesa {pedido.Mesa.Numero} e enviado para a cozinha", User);
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
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Consulta pedido {idPedido}", User);
                return Ok(pedido);
            } catch (Exception ex) {
                return BadRequest(ex.ToString());
            }
        } 
    }
}
