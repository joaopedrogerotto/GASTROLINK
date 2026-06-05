using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class CategoriaPratoController : ControllerBase {
        private readonly IFacadeCategoriaPrato _facade;

        public CategoriaPratoController(IFacadeCategoriaPrato facade) {
            _facade = facade;
        }

        [HttpPost]
        public IActionResult CadastrarCategoriaPrato(CategoriaPrato categoriaPrato) {
            try {
                _facade.CadastrarCategoriaPrato(categoriaPrato);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
