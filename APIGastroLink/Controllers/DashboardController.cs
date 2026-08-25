using APIGastroLink.DTO;
using APIGastroLink.Enums;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    [Authorize(Policy = "SomenteAdmin")]
    public class DashboardController : ControllerBase {
        private readonly IFacadeDashboard _facadeDashboad;
        private readonly IFacadeAuditoria _facadeAuditoria;

        public DashboardController(IFacadeDashboard facadeDashboard, IFacadeAuditoria facadeAuditoria) {
            _facadeDashboad = facadeDashboard;
            _facadeAuditoria = facadeAuditoria;
        }

        [HttpPost]
        public async Task<IActionResult> Dashboard([FromBody]DashboardFiltroDTO DashboardFiltroDTO) {
            try {
                var indicadores = _facadeDashboad.GerarIndicadores(DashboardFiltroDTO);
                await _facadeAuditoria.RegistrarAuditoria(AcaoAuditoriaEnum.Consulta, "Consulta o dashboard", User);
                return Ok(indicadores);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> ResumoVenda() {
            try {
                var listVendas = await _facadeDashboad.GerarResumoVenda();
                return Ok(listVendas);
            }catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
