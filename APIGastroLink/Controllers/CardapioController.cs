using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class CardapioController : ControllerBase {
        private readonly IFacadeCardapio _facadeCardapio;

        public CardapioController(IFacadeCardapio facadeCardapio) {
            _facadeCardapio = facadeCardapio;
        }

        [HttpGet]
        public async Task<IActionResult> GetCardapio() {
            var cardapio = await _facadeCardapio.SelecionarCardapio();
            return Ok(cardapio);
        }
    }
}
