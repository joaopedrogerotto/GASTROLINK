namespace GastroLink.DTO {
    public class RegistrarPagamentoDTO {
        public decimal Desconto { get; set; }
        public decimal ValorTotal { get; set; }
        public int IdPedido { get; set; }
        public List<PagamentoDTO> Pagamentos { get; set; } = new List<PagamentoDTO>();
        public int IdUsuario { get; set; }
    }
}
