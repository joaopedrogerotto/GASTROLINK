using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class MesaController : ControllerBase {
        private readonly IFacadeMesa _facadeMesa;

        public MesaController(IFacadeMesa facadeMesa) {
            _facadeMesa = facadeMesa;
        }


        [HttpPost("SalvarMesa")]
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

        [HttpPost("SalvarLayout")]
        public IActionResult SalvarLayoutMesas(List<LayoutMesaDTO> layout) {
            try {
                var listMesa = new List<Mesa>();
                foreach (var mesa in layout) {
                    listMesa.Add(new Mesa {
                        Id = mesa.Id,
                        PosicaoX = mesa.PosicaoX,
                        PosicaoY = mesa.PosicaoY
                    });
                }

                _facadeMesa.AtualizarLayoutMesas(listMesa);

                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
