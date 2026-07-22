import { PedidoHub } from "./Hubs/pedidoHub.js";

interface StatusPedido{
    IdPedido: Number,
    IdStatusPedido: Number
}

const hub = new PedidoHub("pedidoHub");

await hub.startConnection();

hub.onNovoPedido(pedido => {
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

    container?.insertAdjacentHTML(
        "beforeend",        `<div class="card">
            <div class="card-header">
                Pedido #${pedido.id}
            </div>
            <div class="card-body">
                <h5 class="card-title">Mesa ${pedido.mesa.numero}</h5>
                ${itensPedido}
                <a href="#" class="btn btn-primary">PRONTO</a>
            </div>
        </div>
        <br>`
    )
}

document.addEventListener("click", (e) => {
    const btnPedidoPronto = (e.target as HTMLElement).closest("#btnPedidoPronto") as HTMLElement | null;

    if (!btnPedidoPronto) {
        return;
    }

    let statusPedido: StatusPedido = { IdPedido: Number(btnPedidoPronto.dataset.idPedido), IdStatusPedido: 4 };

    $.ajax({
        url: '/Pedido/AtualizarStatusPedido',
        method: 'POST',
        data: JSON.stringify(statusPedido),
        contentType: 'application/json',
        success: function () {
            location.reload();
        },
        error: function (xhr, status, error) {
            console.log("Erro:" + error);
        }
    })
})