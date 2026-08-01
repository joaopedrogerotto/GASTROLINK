using GastroLink.Models;

namespace GastroLink.DTO {
    public class DadosPagamentoDTO {
        public List<FormaPagamento> FormasPagamento { get; set; } = new List<FormaPagamento>();
        public Pedido Pedido { get; set; } = new Pedido();
    }
}
