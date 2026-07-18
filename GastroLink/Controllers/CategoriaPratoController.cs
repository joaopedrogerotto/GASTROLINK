using GastroLink.Exceptions;
using GastroLink.Facade.Interface;
using GastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class CategoriaPratoController : Controller {
        private readonly IFacadeCategoriaPrato _facadeCategoria;

        public CategoriaPratoController(IFacadeCategoriaPrato facadeCategoria) {
            _facadeCategoria = facadeCategoria;
        }

        public IActionResult Cadastrar() {
            return View();
        }

        public async Task<IActionResult> TodasCategorias() {
            var categorias = await _facadeCategoria.SelecionarCategoriasComQuantiadadePratos();
            return View(categorias);
        }

        public async Task<JsonResult> TodasCategoriasJson() {
            var categorias = await _facadeCategoria.SelecionarCategoriasComQuantiadadePratos();
            return Json(categorias);
        }

        public async Task<IActionResult> CadastrarCategoria([FromBody] CategoriaPrato categoriaPrato) {
            try {
                var resultado = await _facadeCategoria.SalvarCategoria(categoriaPrato);
                if (resultado) {
                    return Ok();
                }
            } catch (EntityAlreadyExistsException eaEX) {
                return Conflict(new { message = "Categoria já cadastrada" });
            }
            return BadRequest(new { message = "Falha ao cadastrar categoria" });
        }
    }
}
