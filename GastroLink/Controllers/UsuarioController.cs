using GastroLink.Facade.Interface;
using GastroLink.Models;
using GastroLink.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class UsuarioController : Controller {
        private readonly IFacadeTipoUsuario _facadeTipoUsuario;
        private readonly IFacadeUsuario _facadeUsuario;

        public UsuarioController(IFacadeTipoUsuario facadeTipoUsuario, IFacadeUsuario facadeUsuario) {
            _facadeTipoUsuario = facadeTipoUsuario;
            _facadeUsuario = facadeUsuario;
        }

        public async Task<IActionResult> CadastroUsuario() {
            var cadastroUsuarioViewModel = new CadastroUsuarioViewModel();
            cadastroUsuarioViewModel.Usuario = new Usuario();
            cadastroUsuarioViewModel.TiposUsuario = await _facadeTipoUsuario.ObterTodosTiposUsuario();
            return View(cadastroUsuarioViewModel);
        }

        public async Task<IActionResult> TodosUsuarios() {
            var usuarios = await _facadeUsuario.ObterTodosUsuarios();
            return View(usuarios);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarUsuario(Usuario Usuario) {
            if (await _facadeUsuario.CadastrarUsuario(Usuario)) {
                TempData["SucessoCadastro"] = "Usuario cadastrado com sucesso.";
                return RedirectToAction("CadastroUsuario");
            }

            TempData["FalhaCadastro"] = "Falha ao cadastrar usuario.";
            return RedirectToAction("CadastroUsuario");
        }

        [HttpGet]
        public async Task<IActionResult> VisualizarUsuario(int idUsuario) {
            var usuario = await _facadeUsuario.ObterUsuarioId(idUsuario);
            return PartialView("_VisualizarUsuario",usuario);
        }
    }
}
