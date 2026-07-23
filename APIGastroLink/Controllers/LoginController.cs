using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using APIGastroLink.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {

    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [AllowAnonymous]

    public class LoginController : ControllerBase {
        private readonly IFacadeLogin _facade;
        private readonly TokenJwtService _tokenJwtService;
        public LoginController(IFacadeLogin facadeLogin, TokenJwtService tokenJwtService) {
            _facade = facadeLogin;
            _tokenJwtService = tokenJwtService;
        }


        [HttpPost]
        public ActionResult Login(Login Login) {
            if (Login == null) {
                return BadRequest("Login não pode ser nulo");
            }

            var usuario = _facade.ValidarLogin(Login);

            if (usuario == null) {
                return BadRequest("Login e/ou Senha inválido");
            }

            var token = _tokenJwtService.GeraToken(usuario.Id.ToString(), usuario.Login, new[] { usuario.Tipo.Tipo });
           

            var usuarioToken = new UsuarioTokenDTO {
                Usuario = usuario,
                Token = token
            };

            return Ok(usuarioToken);
        }
    }
}
