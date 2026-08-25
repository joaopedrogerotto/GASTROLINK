using APIGastroLink.Enums;
using APIGastroLink.Exceptions;
using APIGastroLink.Facade.Interface;
using APIGastroLink.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class CategoriaPratoController : ControllerBase {
        private readonly IFacadeCategoriaPrato _facade;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public CategoriaPratoController(IFacadeCategoriaPrato facade, IFacadeAuditoria facadeAuditoria) {
            _facade = facade;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        [Authorize(Policy = "SomenteAdmin")]
        public async Task<IActionResult> CadastrarCategoriaPrato(CategoriaPrato categoriaPrato) {
            try {
                _facade.CadastrarCategoriaPrato(categoriaPrato);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Criado a categoria de pratos {categoriaPrato.Categoria}", User);
                return Ok();
            } catch (EntityAlreadyExistsException ex) {
                return Conflict(new { message = ex.Message });
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro interno: " + ex.Message });
            }
        }

        [HttpGet("TodasCategorias")]
        public async Task<IActionResult> GetTodasCategorias() {
            try {
                var categorias = _facade.SelecionarTodasCategorias();
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Consulta todas categorias de pratos", User);
                return Ok(categorias);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro interno: " + ex.Message });
            }
        }

        [HttpGet("QuantidadePratos")]
        public async Task<IActionResult> GetCategoriaQuantidadePratos() {
            try {
                var categoriasPrato = _facade.SelecionarCategoriaQuantidadePratos();
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Criacao, $"Consultado a quantidade de pratos para cada categoria", User);
                return Ok(categoriasPrato);
            } catch (Exception ex) {
                return StatusCode(500, new { Message = "Erro interno: " + ex.Message });
            }
        }
    }
}
