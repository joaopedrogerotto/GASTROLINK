using Microsoft.AspNetCore.SignalR;

namespace APIGastroLink.Hubs {
    public class PedidoHub : Hub {
        public override async Task OnConnectedAsync() {
            Console.WriteLine($"Conectou: {Context.ConnectionId}");
            await Groups.AddToGroupAsync(Context.ConnectionId, "COZINHA");
            await Groups.AddToGroupAsync(Context.ConnectionId, "GARCOM");
            await Groups.AddToGroupAsync(Context.ConnectionId, "CAIXA");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "COZINHA");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "GARCOM");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "CAIXA");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
