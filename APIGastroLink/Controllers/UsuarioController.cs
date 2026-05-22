using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class UsuarioController : ControllerBase {
        private readonly IFacadeUsuario _facadeUsuario;

        public UsuarioController(IFacadeUsuario facadeUsuario) {
            _facadeUsuario = facadeUsuario;
        }

        [HttpPost]
        public IActionResult SalvarUsuario(UsuarioCreateDTO Usuario) {
            try {
                _facadeUsuario.InserirUsuario(Usuario);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public IActionResult ListarUsuarios() {
            try {
                var usuarios = _facadeUsuario.ObterTodosUsuarios();
                return Ok(usuarios);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idUsuario}")]
        public IActionResult ObterUsuarioPeloId(int idUsuario) {
            try {
                var usuario = _facadeUsuario.ObterUsuarioPeloId(idUsuario);
                return Ok(usuario);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult AtualizarUsuario(UsuarioUpdateDTO UsuarioUpdateDTO) {
            try {
                _facadeUsuario.AtualizarUsuario(UsuarioUpdateDTO);
                return Ok();

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("alterar-status")]
        public IActionResult AlterarStatusUsuario(UsuarioStatusUpdateDTO usuarioStatusUpdateDTO) {
            try {
                _facadeUsuario.AlterarStatusUsuario(usuarioStatusUpdateDTO);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
