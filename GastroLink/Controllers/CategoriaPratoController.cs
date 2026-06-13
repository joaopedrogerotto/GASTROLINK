using GastroLink.Facade.Interface;
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
    }
}
