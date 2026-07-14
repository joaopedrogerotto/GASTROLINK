import { PedidoHub } from "./Hubs/pedidoHub.js";

const hub = new PedidoHub("pedidoHub");

await hub.startConnection();

hub.onNovoPedido(pedido => {
    console.log(pedido);
    adicionarNovoPedidoNaTela(pedido);
});

function adicionarNovoPedidoNaTela(pedido: any): void {
    console.log(pedido);
    const container = document.getElementById("containerPedidos");

    container?.insertAdjacentHTML(
        "beforeend",
        `<div class="card">
            <div class="card-header">
                Pedido #${pedido.id}
            </div>
            <div class="card-body">
                <h5 class="card-title"></h5>
                <p class="card-text">TESTE</p>
                <a href="#" class="btn btn-primary">TESTE</a>
            </div>
        </div>
        <br>`
    )
}