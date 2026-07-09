using GastroLink.Models;
using GastroLink.Service.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace GastroLink.Service {
    public class RascunhoPedidoService : IRascunhoPedidoService {
        private readonly IDatabase _database;

        public RascunhoPedidoService(IConnectionMultiplexer redis) {
            _database = redis.GetDatabase();
        }

        public async Task SalvarRascunho(RascunhoPedido RascunhoPedido) {
            var jsonPedidoRascunho = JsonSerializer.Serialize(RascunhoPedido);

            await _database.StringSetAsync($"pedido:mesa:{RascunhoPedido.MesaId}", jsonPedidoRascunho, TimeSpan.FromHours(1));
        }

        public async Task<RascunhoPedido> ObterRascunhoPedido(int mesaId) {
            var jsonPedidoRascunho = await _database.StringGetAsync($"pedido:mesa:{mesaId}");

            if (string.IsNullOrEmpty(jsonPedidoRascunho)) {
                return null;
            }

            return JsonSerializer.Deserialize<RascunhoPedido>(jsonPedidoRascunho);
        }

        public int ObterQuantidadePratos(List<RascunhoItemPedido> itens) => itens.Sum(item => item.Quantidade);

        public async Task RemoverRascunho(int mesaId) => await _database.KeyDeleteAsync($"pedido:mesa:{mesaId}");
        
    }
}
