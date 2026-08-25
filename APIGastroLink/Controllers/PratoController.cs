using APIGastroLink.DAO.Interfaces;
using APIGastroLink.DTO;
using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PratoController : ControllerBase {
        private readonly IFacadePrato _facadePrato;
        private readonly IImagemService _imagemService;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public PratoController(IFacadePrato facadePrato, IImagemService imagemService, IFacadeAuditoria facadeAuditoria) {
            _facadePrato = facadePrato;
            _imagemService = imagemService;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        [Authorize(Policy = "AdminGerente")]
        public async Task<IActionResult> CadastrarPrato([FromForm] PratoCreateDTO pratoCreateDTO) {
            try {
                var urlImagem = await _imagemService.UploadImagem(pratoCreateDTO.formFile);
                _facadePrato.CadastrarPrato(pratoCreateDTO, urlImagem);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao,$"Prato com o nome {pratoCreateDTO.Nome} criado", User);
            } catch (Exception ex) {
                return BadRequest($"Erro ao cadastrar prato: {ex.Message}");
            }
            return Ok("Prato cadastrado com sucesso!");
        }

        [HttpGet("TodosPratos")]
        public async Task<IActionResult> TodosPratos([FromQuery] FiltroPesquisaDTO filtroPesquisaDTO) {
            try {
                var listPratos = new List<Prato>();
                if (filtroPesquisaDTO.PossuiFiltro()) {
                    listPratos = await _facadePrato.SelcionarTodosPratos();
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Todos os pratos", User);
                } else {
                    listPratos = await _facadePrato.PesquisarPrato(filtroPesquisaDTO);
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Pesquisa de prato com filtros", User);

                }
                return Ok(listPratos);
            } catch (Exception ex) {
                return BadRequest("Falha ao buscar todos os pratos: " + ex.Message);
            }
        }

        [HttpGet("{idPrato}")]
        public async Task<IActionResult> SelecionarPratoPorId(int idPrato) {
            if (idPrato == 0) {
                return BadRequest("Id não pode ser zero");
            }
            try {
                var prato = await _facadePrato.SelecionarPratoPorId(idPrato);
                if (prato.Id != 0) {
                    await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Seleciona prato pelo id {idPrato}", User);
                    return Ok(prato);
                }

                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, $"Seleciona prato pelo id {idPrato}", User);
                return NotFound("Prato não encontrado");
            } catch (Exception ex) {
                return BadRequest("Falha ao buscar prato: " + ex.Message);
            }
        }

        [HttpPost("AtualizarDisponibilidade")]
        public async Task<IActionResult> AtualizarDisponibilidade(PratoStatusUpdateDTO pratoStatusUpdateDTO) {
            if (pratoStatusUpdateDTO.Id == 0) {
                return BadRequest("Id não pode ser zero");
            }

            try {
                _facadePrato.AtualizarDisponibilidade(pratoStatusUpdateDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Atualizacao, $"Prato do id {pratoStatusUpdateDTO.Id} teve sua disponibilidade alterada para {(pratoStatusUpdateDTO.Status ? "Diposnivel": "Indisponivel")}", User);

                return Ok("Disponibilidade atualizada");
            } catch (Exception ex) {
                return BadRequest("Falha ao atualizar disponibilidade");
            }
        }

        [HttpPut("AtualizarPrato")]
        public async Task<IActionResult> AtualizarPrato([FromForm] PratoEditarDTO pratoEditarDTO) {
            if (pratoEditarDTO.Id == 0) {
                return BadRequest("Id não pode ser zero");
            }
            try {
                if (pratoEditarDTO.formFile != null) {
                    var urlImagem = await _imagemService.UploadImagem(pratoEditarDTO.formFile);
                    pratoEditarDTO.UrlImagem = urlImagem;
                }

                await _facadePrato.AtualizarPrato(pratoEditarDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Edicao, $"Prato do id {pratoEditarDTO.Id} teve suas informações editadas", User);

                return Ok("Prato atualizado com sucesso");
            } catch (Exception ex) {
                return BadRequest("Falha ao atualizar prato: " + ex.Message);
            }
        }
    }
}
