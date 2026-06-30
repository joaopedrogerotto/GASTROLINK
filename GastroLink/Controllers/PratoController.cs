using GastroLink.DTO;
using GastroLink.Enums;
using GastroLink.Exceptions;
using GastroLink.Facade.Interface;
using GastroLink.Mapper;
using GastroLink.Models;
using GastroLink.Service;
using GastroLink.Settings;
using GastroLink.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Runtime.CompilerServices;

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
            var listCategorias = await _facadeCategoriaPrato.SelecionarCategorias();
            return View(listCategorias);
        }

        public async Task<IActionResult> ListaPratos([FromBody] FiltroPesquisaDTO? filtroPesquisaDTO) { 
            try {
                var listPratos = filtroPesquisaDTO == null ? await _facadePrato.SelecionarTodosPratos() : await PesquisarPrato(filtroPesquisaDTO);

                DefinirCaminhoImagem(listPratos);

                return PartialView("_ListaPratos",listPratos);
            } catch (InvalidOperationException iEx) {
                TempData["Falha"] = iEx.Message;
                return PartialView("_ListaPratos",new List<Prato>());
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
            }catch (InvalidExtensionException ieEx) {
                TempData["ErroCadPrato"] = ieEx.Message;
            }

            return RedirectToAction("Cadastrar", "Prato"); 
        }

        public async Task<IActionResult> VisualizarPrato(int idPrato) {
            var prato = new Prato();
            try {
                prato = await _facadePrato.BuscarPratoPorId(idPrato);
                DefinirCaminhoImagem(prato);   
            } catch {
                prato = null;
            }
            return PartialView("_VisualizarPrato", prato);
        }

        [HttpPost]
        public async Task<IActionResult> AtualizarDisponibilidade([FromBody] PratoStatusUpdateDTO pratoStatusUpdateDTO) {
            await _facadePrato.AtualizarDisponibilidade(pratoStatusUpdateDTO);
            return Ok();
        }

        private async Task<List<Prato>> PesquisarPrato(FiltroPesquisaDTO FiltroPesquisaDTO) {
            var listPrato = await _facadePrato.SelecionarPratosPesquisa(FiltroPesquisaDTO);
            return listPrato;
        }

        private void DefinirCaminhoImagem(List<Prato> listPratos) {
            foreach (var prato in listPratos) {
                prato.UrlImagem = $"{_apiSettings.BaseUrlImagem}{prato.UrlImagem}";
            }
        }

        private void DefinirCaminhoImagem(Prato Prato) {
            Prato.UrlImagem = $"{_apiSettings.BaseUrlImagem}{Prato.UrlImagem}";
        }
    }
}