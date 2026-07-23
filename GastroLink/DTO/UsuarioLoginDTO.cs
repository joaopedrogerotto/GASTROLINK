using GastroLink.Models;

namespace GastroLink.DTO {
    public class UsuarioLoginDTO {
        public string Token { get; set; }
        public Usuario Usuario { get; set; }
    }
}
