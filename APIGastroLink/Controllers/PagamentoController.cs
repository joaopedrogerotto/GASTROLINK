using APIGastroLink.DTO;
using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "Caixa")]
    public class PagamentoController : ControllerBase {
        private readonly IFacadePagamento _facadePagamento;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public PagamentoController(IFacadePagamento facadePagamento, IFacadeAuditoria facadeAuditoria) {
            _facadePagamento = facadePagamento;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPagamento([FromBody] RegistrarPagamentoDTO pagamentoRequestDTO) {

            try {
                var result = await _facadePagamento.RegistrarPagamento(pagamentoRequestDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Pagamento, $"Pagamento registrado para o pedido {pagamentoRequestDTO.IdPedido} no valor total de {pagamentoRequestDTO.ValorTotal} e foi pago {pagamentoRequestDTO.Pagamentos.Sum(p => p.ValorPago)} com o desconto de {pagamentoRequestDTO.Desconto}", User);
                if (result) {
                    return Ok();
                }
                return BadRequest();
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpPost("GerarQrCodePix")]
        public async Task<IActionResult> GerarQRCodePix([FromBody] PagamentoPixDTO pagamentoRequestDTO) {
            try {
                var qrCodeResponse = await _facadePagamento.GerarQRCodePix(pagamentoRequestDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Cria QrCode no valor de {pagamentoRequestDTO.ValorPagoPix} para o pedido {pagamentoRequestDTO.IdPedido}",User);
                return Ok(qrCodeResponse);
            } catch (Exception ex) {
                return BadRequest();
            }
        }

        [HttpPost("VerificarQrCode")]
        public async Task<IActionResult> VerificarQrCode([FromBody] PedidoPixDTO pedidoPixDTO) {
            try {
                var result = await _facadePagamento.VerificarQrCode(pedidoPixDTO);
                if (result) {
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"QrCode pago no valor de {pedidoPixDTO.ValorPago} para o pedido {pedidoPixDTO.IdPedido}", User);
                    return Ok(1);
                }
                return Ok(0);
            } catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}
