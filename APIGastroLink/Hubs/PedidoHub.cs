using Microsoft.AspNetCore.SignalR;

namespace APIGastroLink.Hubs {
    public class PedidoHub : Hub {
        public override async Task OnConnectedAsync() {
            await Groups.AddToGroupAsync(Context.ConnectionId, "COZINHA");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "COZINHA");

            await base.OnDisconnectedAsync(exception);
        }
    }
}
