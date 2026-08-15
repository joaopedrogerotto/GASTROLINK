using APIGastroLink.DTO;
using APIGastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace APIGastroLink.Controllers {
    [ApiController]
    [Route("api-gastrolink/[controller]")]
    public class DashboardController : ControllerBase {
        public IFacadeDashboard _facadeDashboad;

        public DashboardController(IFacadeDashboard facadeDashboard) {
            _facadeDashboad = facadeDashboard;
        }

        [HttpPost]
        public IActionResult Dashboard([FromBody]DashboardFiltroDTO DashboardFiltroDTO) {
            try {
                var indicadores = _facadeDashboad.GerarIndicadores(DashboardFiltroDTO);
                return Ok(indicadores);
            } catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}
