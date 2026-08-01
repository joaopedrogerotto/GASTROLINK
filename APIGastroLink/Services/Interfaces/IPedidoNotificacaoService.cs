using APIGastroLink.Models;

namespace APIGastroLink.Services.Interfaces {
    public interface IPedidoNotificacaoService {
        Task NovoPedido(Pedido pedido);
        Task PedidoPronto(Pedido pedido);
        Task AguardandoPagamento(Pedido Pedido);
    }
}
