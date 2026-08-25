using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "Cozinha")]
    public class CozinhaController : ControllerBase {
        private readonly IFacadePedido _facadePedido;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public CozinhaController(IFacadePedido facadePedido, IFacadeAuditoria facadeAuditoria) {
            _facadePedido = facadePedido;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpGet("pedidos")]
        public async Task<IActionResult> GetTodosPedidos() {
            try {
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Consulta todos os pedidos da cozinha", User);
                return Ok(await _facadePedido.SelecionarPedidosCozinha());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
