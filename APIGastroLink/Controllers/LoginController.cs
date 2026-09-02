using APIGastroLink.DTO;
using APIGastroLink.Enums;
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
        private readonly IFacadeAuditoria _facadeAuditoria;

        public LoginController(IFacadeLogin facadeLogin, TokenJwtService tokenJwtService, IFacadeAuditoria facadeAuditoria) {
            _facade = facadeLogin;
            _tokenJwtService = tokenJwtService;
            _facadeAuditoria = facadeAuditoria;
        }


        [HttpPost]
        public async Task<ActionResult> Login(Login Login) {
            if (Login == null) {
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Login, $"Tentativa de login nulo.");
                return BadRequest("Login não pode ser nulo");
            }

            var usuario = _facade.ValidarLogin(Login);

            if (usuario == null) {
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Login, $"Tentativa de login com os usuarios {Login.LoginStr} e senha {Login.Senha}. Login e/ou Senha inválido");
                return BadRequest("Login e/ou Senha inválido");
            }

            var token = _tokenJwtService.GeraToken(usuario.Id.ToString(), usuario.Login, new[] { usuario.Tipo.Tipo });
           

            var usuarioToken = new UsuarioTokenDTO {
                Usuario = usuario,
                Token = token
            };
            

            await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Login,$"Login efetuado com login {usuario.Login}");

            return Ok(usuarioToken);
        }
    }
}
