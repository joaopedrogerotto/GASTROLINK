using APIGastroLink.DTO;
using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class UsuarioController : ControllerBase {
        private readonly IFacadeUsuario _facadeUsuario;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public UsuarioController(IFacadeUsuario facadeUsuario, IFacadeAuditoria facadeAuditoria) {
            _facadeUsuario = facadeUsuario;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        public async Task<IActionResult> SalvarUsuario(UsuarioCreateDTO Usuario) {
            try {
                _facadeUsuario.InserirUsuario(Usuario);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Criação do usuario {Usuario.Nome}, identificado por {Usuario.Login} e tipo de usuario representado pelo id {Usuario.TipoUsuarioId}", User) ;
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ListarUsuarios() {
            try {
                var usuarios = _facadeUsuario.ObterTodosUsuarios();
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta todos os usuarios", User);
                return Ok(usuarios);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{idUsuario}")]
        public async Task<IActionResult> ObterUsuarioPeloId(int idUsuario) {
            try {
                var usuario = _facadeUsuario.ObterUsuarioPeloId(idUsuario);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Consulta usuario {usuario.Nome} pelo id Usuario {idUsuario}", User);
                return Ok(usuario);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AtualizarUsuario")]
        public async Task<IActionResult> AtualizarUsuario(UsuarioUpdateDTO UsuarioUpdateDTO) {
            try {
                _facadeUsuario.AtualizarUsuario(UsuarioUpdateDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Edicao, $"Usuario atualiado pelo id {UsuarioUpdateDTO.Id}", User);
                return Ok();

            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("AlterarStatus")]
        public async Task<IActionResult> AlterarStatusUsuario(UsuarioStatusUpdateDTO usuarioStatusUpdateDTO) {
            try {
                _facadeUsuario.AlterarStatusUsuario(usuarioStatusUpdateDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Atualizacao, $"Status do usuario representado pelo id {usuarioStatusUpdateDTO.Id} alterado para {(usuarioStatusUpdateDTO.Status ? "Ativo" : "Inativo")}", User); 
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
