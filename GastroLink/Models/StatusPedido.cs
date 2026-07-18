namespace GastroLink.Models {
    public class StatusPedido {
        public int Id { get; set; }
        public string Status { get; set; }
        public List<Pedido> Pedidos { get; set; }
    }
}