using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "Atendimento")]
    public class CardapioController : ControllerBase {
        private readonly IFacadeCardapio _facadeCardapio;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public CardapioController(IFacadeCardapio facadeCardapio, IFacadeAuditoria facadeAuditoria) {
            _facadeCardapio = facadeCardapio;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpGet]
        public async Task<IActionResult> GetCardapio() {
            var cardapio = await _facadeCardapio.SelecionarCardapio();
            await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta cardapio", User);
            return Ok(cardapio);
        }
    }
}
