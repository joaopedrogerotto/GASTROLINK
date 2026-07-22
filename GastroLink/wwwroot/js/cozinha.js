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
    const data = new Date(pedido.dataCriacao);
    const dataFormatada = data.toLocaleString("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit",
        hour12: false
    });
    container?.insertAdjacentHTML("beforeend", `<div class="card card-pedido">
            <div class="card-header d-flex justify-content-between align-items-center">
                <span>Pedido #${pedido.id}</span>
                <small class="text-muted">
                    ${dataFormatada}
                </small>
            </div>
            <div class="card-body">
                <h5 class="card-title">Mesa ${pedido.mesa.numero}</h5>
                ${itensPedido}
                 <button href="#" class="btn btn-info" data-id-pedido="${pedido.id}" data-id-status="3" id="btnPedidoPronto">EM PREPARO</button>
            </div>
        </div>
        <br>`);
}
document.addEventListener("click", (e) => {
    const btnPedidoPronto = e.target.closest("#btnPedidoPronto");
    if (!btnPedidoPronto) {
        return;
    }
    const carPedido = btnPedidoPronto.closest(".card-pedido");
    let idStatus = Number(btnPedidoPronto.dataset.idStatus);
    let idPedido = Number(btnPedidoPronto.dataset.idPedido);
    let statusPedido = { IdPedido: idPedido, IdStatusPedido: idStatus };
    $.ajax({
        url: '/Pedido/AtualizarStatusPedido',
        method: 'POST',
        data: JSON.stringify(statusPedido),
        contentType: 'application/json',
        success: function () {
            if (idStatus === 4) {
                carPedido?.remove();
            }
            else if (idStatus === 3) {
                btnPedidoPronto.textContent = "PRONTO";
                btnPedidoPronto.classList.remove("btn-info");
                btnPedidoPronto.classList.add("btn-success");
                btnPedidoPronto.dataset.idStatus = "4";
            }
        },
        error: function (xhr, status, error) {
            console.log("Erro:" + error);
        }
    });
});
//# sourceMappingURL=cozinha.js.map