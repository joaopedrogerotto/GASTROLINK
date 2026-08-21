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

            var usuarioToken = await _facadeLogin.ValidarLogin(Login);

            if (usuarioToken == null) {
                TempData["FalhaLogin"] = "Login e/ou Senha incorretos.";
                return View("Index");
            }
            var claims = new List<Claim> {
                new Claim(ClaimTypes.Name, usuarioToken.Usuario.Login),
                new Claim(ClaimTypes.Role, usuarioToken.Usuario.Tipo.Tipo),
                new Claim("jwt_token", usuarioToken.Token)
            };


            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);
            var authenticationProperites = new AuthenticationProperties { IsPersistent = true, ExpiresUtc = DateTimeOffset.UtcNow.AddHours(1) };

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authenticationProperites);


            HttpContext.Session.SetString("NomeUsuario", usuarioToken.Usuario.Nome);
            HttpContext.Session.SetInt32("IdUsuario", usuarioToken.Usuario.Id);
            HttpContext.Session.SetInt32("IdTipoUsuario", usuarioToken.Usuario.Tipo.Id);
            HttpContext.Session.SetString("TipoUsuarioStr", usuarioToken.Usuario.Tipo.Tipo);

            if (usuarioToken.Usuario.Tipo.Tipo == "COZINHA") {
                return RedirectToAction("TodosPedidos", "Cozinha");
            } else if (usuarioToken.Usuario.Tipo.Tipo == "CAIXA") {
                return RedirectToAction("TodosPedidos", "Caixa");
            } else if (usuarioToken.Usuario.Tipo.Tipo == "CHATBOT") {
                return RedirectToAction("Chatbot", "Chatbot");
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
