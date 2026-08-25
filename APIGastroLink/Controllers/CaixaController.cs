using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Factory.Interfaces;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class CaixaController : ControllerBase {
        private readonly IFacadePedido _facadePedido;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public CaixaController(IFacadePedido facadePedido, IFacadeAuditoria facadeAuditoria) {
            _facadePedido = facadePedido;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpGet]
        [Authorize("Caixa")]
        public async Task<IActionResult> TodosPedidosAsync() {
            try {
                var pedidos = await _facadePedido.SelecionaPedidosCaixa();

                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta aos pedidos do caixa", User);

                return Ok(pedidos);
            }catch(Exception ex) {
                return BadRequest(ex.ToString());
            }
        }
    }
}
