using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class PratoController : Controller {
        private readonly IFacadeCategoriaPrato _facadeCategoriaPrato;
        private readonly IFacadePrato _facadePrato;

        public PratoController(IFacadeCategoriaPrato facadeCategoriaPrato, IFacadePrato facadePrato) {
            _facadeCategoriaPrato = facadeCategoriaPrato;
            _facadePrato = facadePrato;
        }

        public async Task<IActionResult> Cadastrar() {
            var viewModel = new CadastroPratoViewModel {
                ListCategorias = await _facadeCategoriaPrato.SelecionarCategorias()
            };
            return View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarPrato(CadastroPratoViewModel viewModel) {
            if (!ModelState.IsValid) {
                viewModel.ListCategorias = await _facadeCategoriaPrato.SelecionarCategorias();
                return View("Cadastrar", viewModel);
            }

            try {
                if (await _facadePrato.CadastrarPrato(viewModel.Prato)) {
                    TempData["SucessoCadPrato"] = "Prato cadastrado com sucesso!";
                } else {
                    TempData["ErroCadPrato"] = "Falha ao cadastrar prato.";
                }
            }catch (ArgumentException ex) {
                TempData["ErroCadPrato"] = ex.Message;
            }

            return RedirectToAction("Cadastrar", "Prato");
        }
    }
}
