using APIGastroLink.DTO;
using APIGastroLink.Models;

namespace APIGastroLink.Services.Interfaces {
    public interface IPedidoNotificacaoService {
        Task NovoPedido(Pedido pedido);
    }
}
