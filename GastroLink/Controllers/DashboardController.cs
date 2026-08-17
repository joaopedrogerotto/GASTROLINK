using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class DashboardController : Controller{
        private IFacadeDashboard _facadeDashboard;

        public DashboardController(IFacadeDashboard facadeDashboard) {
            _facadeDashboard = facadeDashboard;
        }

        public async Task<IActionResult> Index() {
            var resumoVendas = await _facadeDashboard.SeleiconarPratoMaisVendidos();
            return View(resumoVendas);
        }
    }
}
