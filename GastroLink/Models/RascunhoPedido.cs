namespace GastroLink.Models {
    public class RascunhoPedido {
        public List<RascunhoItemPedido> Itens { get; set; } = new List<RascunhoItemPedido>();
        public int MesaId { get; set; }
    }
}
