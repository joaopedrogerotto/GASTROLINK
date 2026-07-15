import { PedidoHub } from "./Hubs/pedidoHub.js";
const hub = new PedidoHub("pedidoHub");
await hub.startConnection();
hub.onNovoPedido(pedido => {
    console.log(pedido);
    adicionarNovoPedidoNaTela(pedido);
});
function adicionarNovoPedidoNaTela(pedido) {
    console.log(pedido);
    const container = document.getElementById("containerPedidos");
    const itensPedido = pedido.itens.map((item) => `
        <p class="card-text">Prato: ${item.prato.nome}</p>
        <p class="card-text">Quantidade: ${item.quantidade}</p>
        <p class="card-text">Observação: ${item.observacao}</p>
        <hr>
    `).join("");
    container?.insertAdjacentHTML("beforeend", `<div class="card">
            <div class="card-header">
                Pedido #${pedido.id}
            </div>
            <div class="card-body">
                <h5 class="card-title">Mesa ${pedido.mesa.numero}</h5>
                ${itensPedido}
                <a href="#" class="btn btn-primary">TESTE</a>
            </div>
        </div>
        <br>`);
}
//# sourceMappingURL=cozinha.js.map