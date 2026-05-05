using GastroLink.Models;

namespace GastroLink.ViewModel {
    public class CadastroUsuarioViewModel {
        public Usuario Usuario { get; set; } = new Usuario();
        public List<TipoUsuario> TiposUsuario { get; set; } = new List<TipoUsuario>();
    }
}
