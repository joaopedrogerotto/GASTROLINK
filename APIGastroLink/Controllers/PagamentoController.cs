using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize("Caixa")]
    public class PagamentoController : ControllerBase {
        private readonly IFacadePagamento _facadePagamento;

        public PagamentoController(IFacadePagamento facadePagamento) {
            _facadePagamento = facadePagamento;
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarPagamento([FromBody] PagamentoRequestDTO pagamentoRequestDTO) {
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
        public async Task<IActionResult> GerarQRCodePix([FromBody] PagamentoRequestDTO pagamentoRequestDTO) {
            try {
                var qrCodeResponse = await _facadePagamento.GerarQRCodePix(pagamentoRequestDTO);
                return Ok(qrCodeResponse);
            } catch (Exception ex) {
                return BadRequest();
            }
        }
    }
}
