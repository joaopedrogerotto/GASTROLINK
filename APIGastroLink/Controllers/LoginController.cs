using APIGastroLink.DAO.Interfaces;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {

    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class LoginController : ControllerBase {
        private readonly IFacadeLogin _facade;
        public LoginController(IFacadeLogin facadeLogin) {
            _facade = facadeLogin;
        }


        [HttpPost]
        public ActionResult Login(Login Login) {
            if(Login == null) {
                return BadRequest("Login não pode ser nulo");
            }

            var usuario = _facade.ValidarLogin(Login);

            if(usuario == null) {
                return BadRequest("Usuario ou senha inválido");
            }

            return Ok(usuario);
        }
    }
}
