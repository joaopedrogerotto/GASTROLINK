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
        public async Task<IActionResult> CarregarPagamento(int id) {
            try {
                var dadosPagamento = await _facadePagamento.ObterDadosParaPagamento(id);
                return Ok(dadosPagamento);
            } catch (Exception ex) {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPagamento([FromBody] RegistrarPagamentoDTO pagamentoRequest) {
            try {
                var valorTotalPago = pagamentoRequest.Pagamentos.Sum(p => p.ValorPago);

                if(valorTotalPago != (pagamentoRequest.ValorTotal - pagamentoRequest.Desconto)) {
                    return BadRequest(new { msg = "Valor total dos pagamentos não corresponde ao valor total do pedido" });
                }

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


        [HttpPost]
        public async Task<IActionResult> GerarQrCodePix([FromBody] PagamentoPixDTO pagamentoRequest) {
            try {
                var qrCode = await _facadePagamento.GerarQrCodePix(pagamentoRequest);
                return Ok(qrCode);
            } catch (Exception ex) {
                return BadRequest(new { msg = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> VerificarQrCode([FromBody] PedidoPixDTO pedidoPix) {
            try {
                var resultado = await _facadePagamento.VerificarStatusPagamentoQrCode(pedidoPix);
                return Ok(resultado);
            } catch (Exception ex) {
                return BadRequest(new { msg = ex.Message });
            }
        }
    }
}
