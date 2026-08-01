using GastroLink.DTO;
using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class PagamentoController : Controller {
        private readonly IFacadePagamento _facadePagamento;

        public PagamentoController(IFacadePagamento facadePagamento) {
            _facadePagamento = facadePagamento;
        }

        [HttpGet]
        public async Task<IActionResult> CarregarPagamento (int id) {
            try {
                var dadosPagamento = await _facadePagamento.ObterDadosParaPagamento(id);
                return Ok(dadosPagamento);
            } catch (Exception ex) {
                return BadRequest(new { msg = ex.Message});
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPagamento([FromBody] PagamentoRequestDTO pagamentoRequest) {
            try {
                pagamentoRequest.IdUsuario = HttpContext.Session.GetInt32("IdUsuario") ?? 0;
                var resultado = await _facadePagamento.EfetuarPagamento(pagamentoRequest);
                if (resultado) {
                    return Ok();
                }
                return BadRequest(new { msg = "Falha ao processar o pagamento" });
            } catch (Exception ex) {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
