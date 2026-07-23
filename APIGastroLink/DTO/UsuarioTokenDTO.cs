using APIGastroLink.Models;

namespace APIGastroLink.DTO {
    public class UsuarioTokenDTO {
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
