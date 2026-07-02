using GastroLink.Facade;
using GastroLink.Facade.Interface;
using GastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class LoginController : Controller {
        private readonly IFacadeLogin _facadeLogin;

        public LoginController(IFacadeLogin facadeLogin) {
            _facadeLogin = facadeLogin;
        }

        public IActionResult Index() {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Autenticar(Login Login) {

            var usuario = await _facadeLogin.ValidarLogin(Login);

            if (usuario == null) {
                TempData["FalhaLogin"] = "Login e/ou Senha incorretos.";
                return View("Index");
            }

            HttpContext.Session.SetString("NomeUsuario", usuario.Nome);
            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetInt32("IdTipoUsuario", usuario.Tipo.Id);
            HttpContext.Session.SetString("TipoUsuarioStr", usuario.Tipo.Tipo);

            return RedirectToAction("TodasMesas","Mesa");
        }
    }
}
