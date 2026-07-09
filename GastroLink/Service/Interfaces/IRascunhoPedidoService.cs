using GastroLink.Models;

namespace GastroLink.Service.Interfaces {
    public interface IRascunhoPedidoService {
        public Task SalvarRascunho(RascunhoPedido RascunhoPedido);
        public Task<RascunhoPedido> ObterRascunhoPedido(int mesaId);
        public int ObterQuantidadePratos(List<RascunhoItemPedido> itens);
        public Task RemoverRascunho(int mesaId);
    }
}
