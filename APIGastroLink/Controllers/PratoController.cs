using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class PratoController : ControllerBase {
        private readonly IFacadePrato _facadePrato;
        private readonly IImagemService _imagemService;

        public PratoController(IFacadePrato facadePrato, IImagemService imagemService) {
            _facadePrato = facadePrato;
            _imagemService = imagemService;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarPrato([FromForm] PratoCreateDTO pratoCreateDTO) {
            try {
                var urlImagem = await _imagemService.UploadImagem(pratoCreateDTO.formFile);
                _facadePrato.CadastrarPrato(pratoCreateDTO, urlImagem);
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
                } else {
                    listPratos = await _facadePrato.PesquisarPrato(filtroPesquisaDTO);
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
                    return Ok(prato);
                }
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
                return Ok("Prato atualizado com sucesso");
            } catch (Exception ex) {
                return BadRequest("Falha ao atualizar prato: " + ex.Message);
            }
        }
    }
}
