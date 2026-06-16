using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PratoController : ControllerBase {
        private readonly IFacadePrato _facadePrato;

        public PratoController(IFacadePrato facadePrato) {
            _facadePrato = facadePrato;
        }

        [HttpPost]
        public IActionResult CadastrarPrato(PratoCreateDTO pratoCreateDTO) {
            try {
                _facadePrato.CadastrarPrato(pratoCreateDTO);
            } catch (Exception ex) {
                return BadRequest($"Erro ao cadastrar prato: {ex.Message}");
            }
            return Ok("Prato cadastrado com sucesso!");
        }
    }
}
