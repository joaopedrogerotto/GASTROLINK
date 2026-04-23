using GastroLink.Facade.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class MesaController : Controller {
        private readonly IFacadeMesa _facadeMesa;
        
        public MesaController(IFacadeMesa facadeMesa) {
            _facadeMesa = facadeMesa;
        }

        public IActionResult Cadastrar() {
            return View();
        }
        public async Task<IActionResult> Mapeamento() {
            var listMesa = await _facadeMesa.BuscarMesasMapeamento();
            return View(listMesa);
        }
        public async Task<IActionResult> TodasMesas() {
            var listMesa = await _facadeMesa.BuscarMesasMapeamento();
            return View(listMesa);
        }
    }
}
