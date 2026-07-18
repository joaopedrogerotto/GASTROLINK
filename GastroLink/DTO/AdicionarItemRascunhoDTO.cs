using GastroLink.Models;

namespace GastroLink.DTO {
    public class AdicionarItemRascunhoDTO {
        public int mesaId { get; set; }
        public RascunhoItemPedido RascunhoItemPedido { get; set; }
    }
}
