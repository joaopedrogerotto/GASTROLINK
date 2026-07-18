using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class TipoUsuarioController : ControllerBase {
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;

        public TipoUsuarioController(IFacadeTipoUsuario facadeTipoUsuario) {
            _facadeTipoUsuario = facadeTipoUsuario;
        }

        [HttpGet]
        public IActionResult SelecionarTodosTipoUsuario() {
            try {
                return Ok(_facadeTipoUsuario.SelectAll());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
