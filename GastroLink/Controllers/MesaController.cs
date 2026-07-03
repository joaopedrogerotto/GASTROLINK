using GastroLink.DTO;
using GastroLink.Exceptions;
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

        public async Task<JsonResult> TodasMesasJson() {
            var listMesa = await _facadeMesa.BuscarMesasMapeamento();
            return Json(listMesa);
        }

        [HttpPost]
        public async Task<IActionResult> SalvarMesa([FromBody] MesaRequestDTO MesaRequestDTO) {
            try {
                var resultado = await _facadeMesa.CadastrarMesa(MesaRequestDTO);
                if (!resultado) {
                    return BadRequest("Ocorreu um erro ao cadastrar a mesa");
                }
            } catch (EntityAlreadyExistsException eaEx) {
                return Conflict(eaEx.Message);
            } catch (Exception ex) {
                return BadRequest("Ocorreu um erro ao cadastrar a mesa: " + ex.Message);
            }
            return Ok();
        }

        public async Task<IActionResult> SalvarLayoutMesas([FromBody] List<LayoutMesaDTO> listMesas) {
            if (listMesas.Count() == 0) {
                TempData["FalhaSalvarLayout"] = "Lista de mesas vazio";
                return RedirectToAction("Mapeamento", "Mesa");
            }

            var resultado = await _facadeMesa.SalvarLayoutMesa(listMesas);
            try {
                if (resultado) {
                    TempData["SucessoSalvarLayout"] = "Sucesso ao salvar layout";
                } else {
                    TempData["FalhaSalvarLayout"] = "Falha ao salvar layout";
                }
            } catch (Exception ex) {
                TempData["FalhaSalvarLayout"] = "Ocorreu um erro ao tentar salvar o layout: " + ex.Message;
            }

            return RedirectToAction("Mapeamento", "Mesa");
        }
    }
}
