import { PedidoHub } from "./Hubs/pedidoHub.js";

interface StatusPedido {
    IdPedido: Number,
    IdStatusPedido: Number
}

const hub = new PedidoHub("pedidoHub");

await hub.startConnection();

hub.onPedidoPronto(pedido => {
    console.log(pedido);
    adicionarNovoPedidoNaTela(pedido);
});


function adicionarNovoPedidoNaTela(pedido: any): void {
    console.log(pedido);
    const container = document.getElementById("containerPedidos");

    const itensPedido = pedido.itens.map((item: any) => `
        <p class="card-text">Prato: ${item.prato.nome}</p>
        <p class="card-text">Quantidade: ${item.quantidade}</p>
        <p class="card-text">Observação: ${item.observacao}</p>
        <hr>
    `).join("");


    const data = new Date(pedido.dataCriacao);

    const dataFormatada = data.toLocaleString("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false
    });

    container?.insertAdjacentHTML(
        "beforeend",
        `<div class="card card-pedido">
            <div class="card-header d-flex justify-content-between align-items-center">
                <span>Pedido #${pedido.id}</span>
                <small class="text-muted">
                    ${dataFormatada}
                </small>
            </div>
            <div class="card-body">
                <h5 class="card-title">Mesa ${pedido.mesa.numero}</h5>
                ${itensPedido}
                 <button href="#" class="btn btn-info" data-id-pedido="${pedido.id}" data-id-status="3" id="btnPedidoPronto">ENTREGUE</button>
            </div>
        </div>
        <br>`
    )
}
