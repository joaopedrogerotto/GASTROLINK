using APIGastroLink.DTO;
using APIGastroLink.Enums;
using APIGastroLink.Exceptions;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class MesaController : ControllerBase {
        private readonly IFacadeMesa _facadeMesa;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public MesaController(IFacadeMesa facadeMesa, IFacadeAuditoria facadeAuditoria) {
            _facadeMesa = facadeMesa;
            _facadeAuditoria = facadeAuditoria;
        }


        [HttpPost("SalvarMesa")]
        [Authorize(Policy = "AdminGerente")]
        public async Task<IActionResult> CadastrarMesa([FromBody] MesaRequestDTO Mesa) {
            try {
                _facadeMesa.CadastrarMesa(Mesa.NumeroMesa);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Mesa criada com o numero {Mesa.NumeroMesa}", User);
                return Created();
            } catch (EntityAlreadyExistsException ex) {
                return Conflict(new { message = ex.Message });
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Policy = "AtendimentoComChatbot")]
        public async Task<IActionResult> SelecionarMesas() {
            try {
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta todas as mesas", User);
                return Ok(_facadeMesa.SelecionarTodasMesas());
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("SalvarLayout")]
        [Authorize(Policy = "AdminGerente")]
        public async Task<IActionResult> SalvarLayoutMesas(List<LayoutMesaDTO> layout) {
            try {
                var listMesa = new List<Mesa>();
                foreach (var mesa in layout) {
                    listMesa.Add(new Mesa {
                        Id = mesa.Id,
                        PosicaoX = mesa.PosicaoX,
                        PosicaoY = mesa.PosicaoY
                    });
                }

                _facadeMesa.AtualizarLayoutMesas(listMesa);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Edicao, "Layout das mesas salvo", User);
                return Ok();
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
