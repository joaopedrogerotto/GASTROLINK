using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize("Caixa")]
    public class FormaPagamentoController : ControllerBase {
        private readonly IFacadeFormaPagamento _facadeFormaPagamento;

        public FormaPagamentoController(IFacadeFormaPagamento facadeFormaPagamento) {
            _facadeFormaPagamento = facadeFormaPagamento;
        }


        [HttpGet]
        public async Task<IActionResult> TodasFormasPagamento() {
            try {
                var listFormaPag = await _facadeFormaPagamento.SelecionaTodos();
                return Ok(listFormaPag);
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
