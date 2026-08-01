namespace APIGastroLink.Models {
    public class Pagamento {
        public int Id { get; set; }
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorPago { get; set; }
        public List<FormaPagamento> FormaPagamento { get; set; } = new List<FormaPagamento>();
        public Pedido Pedido { get; set; } = new Pedido();
        public Usuario Usuario { get; set; } = new Usuario();
    }
}
