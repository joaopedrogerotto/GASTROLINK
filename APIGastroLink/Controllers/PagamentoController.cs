using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "Caixa")]
    public class PagamentoController : ControllerBase {
        private readonly IFacadePagamento _facadePagamento;

        public PagamentoController(IFacadePagamento facadePagamento) {
            _facadePagamento = facadePagamento;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPagamento([FromBody] RegistrarPagamentoDTO pagamentoRequestDTO) {
            try {
                var result = await _facadePagamento.RegistrarPagamento(pagamentoRequestDTO);
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
                    return Ok(1);
                }
                return Ok(0);
            } catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}
