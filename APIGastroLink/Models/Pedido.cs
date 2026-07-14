namespace APIGastroLink.Models {
    public class Pedido {
        public int Id { get; set; }
        public Mesa Mesa { get; set; }
        public Usuario Usuario { get; set; }
        public decimal ValorTotal { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
        public StatusPedido Status { get; set; }
        public DateTime dataCriacao { get; set; }
    }
}
