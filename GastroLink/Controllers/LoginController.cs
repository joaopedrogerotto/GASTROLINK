using GastroLink.Facade.Interface;
using GastroLink.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GastroLink.Controllers {
    [AllowAnonymous]
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

            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuario.Nome),
                new Claim(ClaimTypes.Role, usuario.Tipo.Tipo)
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            HttpContext.Session.SetString("NomeUsuario", usuario.Nome);
            HttpContext.Session.SetInt32("IdUsuario", usuario.Id);
            HttpContext.Session.SetInt32("IdTipoUsuario", usuario.Tipo.Id);
            HttpContext.Session.SetString("TipoUsuarioStr", usuario.Tipo.Tipo);

            if (usuario.Tipo.Tipo == "COZINHA") {
                return RedirectToAction("TodosPedidos", "Cozinha");
            }

            return RedirectToAction("TodasMesas", "Mesa");
        }

        public async Task<IActionResult> Logout() {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Login");
        }
    }
}
