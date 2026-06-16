using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PratoController : ControllerBase {
        private readonly IFacadePrato _facadePrato;
        private readonly IImagemService _imagemService;

        public PratoController(IFacadePrato facadePrato, IImagemService imagemService) {
            _facadePrato = facadePrato;
            _imagemService = imagemService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPrato([FromForm]PratoCreateDTO pratoCreateDTO) {
            try {
                var urlImagem = await _imagemService.UploadImagem(pratoCreateDTO.formFile);
                _facadePrato.CadastrarPrato(pratoCreateDTO, urlImagem);
            } catch (Exception ex) {
                return BadRequest($"Erro ao cadastrar prato: {ex.Message}");
            }
            return Ok("Prato cadastrado com sucesso!");
        }
    }
}
