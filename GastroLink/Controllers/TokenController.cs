using Microsoft.AspNetCore.Mvc;

namespace GastroLink.Controllers {
    public class TokenController : Controller {
        public IActionResult ObterToken() {
            var token = User.FindFirst("jwt_token")?.Value;

            if (string.IsNullOrEmpty(token)) {
                return Unauthorized();
            }
            return Json(new {token});
        }
    }
}
