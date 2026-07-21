using GastroLink.Models;

namespace GastroLink.ViewModel {
    public class CriarPedidoViewModel {
        public int idMesa { get; set; }
        public string numeroMesa { get; set; }
        public List<CategoriaPrato> listCategoriaPrato { get; set; }
    }
}
