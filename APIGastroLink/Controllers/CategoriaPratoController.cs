using APIGastroLink.Exceptions;
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
            } catch (EntityAlreadyExistsException ex) {
                return Conflict(new {message = ex.Message});
            } catch (Exception ex) {
                return StatusCode(500, new {Message = "Erro interno: " + ex.Message});
            }
        }

        [HttpGet]
        public IActionResult GetTodasCategorias() {
            try {
                var categorias = _facade.SelecionarTodasCategorias();
                return Ok(categorias);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro interno: " + ex.Message });
            }
        }
    }
}
