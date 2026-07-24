using APIGastroLink.Hubs;
using APIGastroLink.Models;
using APIGastroLink.Services.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace APIGastroLink.Services {
    public class PedidoNotificacaoService : IPedidoNotificacaoService {
        private readonly IHubContext<PedidoHub> _hubContext;

        public PedidoNotificacaoService(IHubContext<PedidoHub> hubContext) {
            _hubContext = hubContext;
        }

        public async Task NovoPedido(Pedido pedido) {
            await _hubContext.Clients.Group("COZINHA").SendAsync("NovoPedido", pedido);
        }

        public async Task PedidoPronto(Pedido pedido) {
            await _hubContext.Clients.Group("GARCOM").SendAsync("PedidoPronto", pedido);
        }
    }
}
