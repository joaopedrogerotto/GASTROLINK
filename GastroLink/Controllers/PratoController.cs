using GastroLink.DTO;
using GastroLink.Facade.Interface;
using GastroLink.Models;
using GastroLink.Settings;
using GastroLink.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace GastroLink.Controllers {
    public class PratoController : Controller {
        private readonly IFacadeCategoriaPrato _facadeCategoriaPrato;
        private readonly IFacadePrato _facadePrato;
        private readonly ApiGastroLinkSettings _apiSettings;

        public PratoController(IFacadeCategoriaPrato facadeCategoriaPrato, IFacadePrato facadePrato, IOptions<ApiGastroLinkSettings> apiSettings) {
            _facadeCategoriaPrato = facadeCategoriaPrato;
            _facadePrato = facadePrato;
            _apiSettings = apiSettings.Value;
        }

        public async Task<IActionResult> Cadastrar() {
            var viewModel = new CadastroPratoViewModel {
                ListCategorias = await _facadeCategoriaPrato.SelecionarCategorias()
            };
            return View(viewModel);
        }

        public async Task<IActionResult> TodosPratos() {
            try {
                var listPratos =  await _facadePrato.SelecionarTodosPratos();

                DefinirCaminhoImagem(listPratos.ToList());

                return View(listPratos);
            } catch (InvalidOperationException iEx) {
                TempData["Falha"] = iEx.Message;
                return View(new List<Prato>());
            }
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
            } catch (ArgumentException ex) {
                TempData["ErroCadPrato"] = ex.Message;
            }

            return RedirectToAction("Cadastrar", "Prato");
        }
        private void DefinirCaminhoImagem(List<Prato> listPratos) {
            foreach (var prato in listPratos) {
                prato.UrlImagem = $"{_apiSettings.BaseUrlImagem}{prato.UrlImagem}";
            }
        }
    }
}