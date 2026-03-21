using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class MesaController : ControllerBase {
        private readonly IFacadeMesa _facadeMesa;

        public MesaController(IFacadeMesa facadeMesa) {
            _facadeMesa = facadeMesa;
        }


        [HttpPost]
        public IActionResult CadastrarMesa([FromBody] MesaRequestDTO Mesa) {
            try {
                _facadeMesa.CadastrarMesa(Mesa.NumeroMesa);
                return Created();
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult SelecionarMesas() {
            try {
                return Ok(_facadeMesa.SelecionarTodasMesas());
            } catch (Exception ex) { 
                return BadRequest(ex.Message);
            }
        }
    }
}
