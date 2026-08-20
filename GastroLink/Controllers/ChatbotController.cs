using GastroLink.Facade.Interface;
using GastroLink.Mapper;
using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class ChatbotController : Controller{
        private readonly IFacadePrato _facadePrato;

        public ChatbotController(IFacadePrato facadePrato) {
            _facadePrato = facadePrato;
        }

        public IActionResult Chatbot() {
            return View();
        }

        public async Task<IActionResult> TodosPratoChatbot() {
            var listPratos = await _facadePrato.SelecionarTodosPratos();
            var listPratoDTO = PratoMapper.ToListPratoChatbotDTO(listPratos);
            return Json(listPratoDTO);
        }
    }
}
