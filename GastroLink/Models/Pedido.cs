namespace GastroLink.Models {
    public class Pedido {
        public int Id { get; set; }
        public Mesa Mesa { get; set; } = new Mesa();
        public Usuario Usuario { get; set; } = new Usuario();
        public decimal ValorTotal { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public StatusPedido Status { get; set; } = new StatusPedido();
        public DateTime dataCriacao { get; set; }
    }
}
