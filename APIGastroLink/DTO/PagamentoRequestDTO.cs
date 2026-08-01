namespace APIGastroLink.DTO {
    public class PagamentoRequestDTO {
        public decimal Desconto { get; set; }
        public decimal ValorPago { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdPedido { get; set; }
        public int IdFormaPagamento { get; set; }
        public int IdUsuario { get; set; }
    }
}
