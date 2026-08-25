using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class GarcomController : ControllerBase {
        private readonly IFacadePedido _facadePedido;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public GarcomController(IFacadePedido facadePedido, IFacadeAuditoria facadeAuditoria) {
            _facadePedido = facadePedido;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpGet]
        [Authorize("Atendimento")]
        public async Task<IActionResult> PedidosProntosAsync() {
            try {
                var pedidos = await _facadePedido.SelecionaPedidosProntos();
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta todos os pedidos prontos para o garçom", User);
                return Ok(pedidos);
            }catch(Exception ex) {
                return BadRequest(ex.ToString());
            }
        }
    }
}
