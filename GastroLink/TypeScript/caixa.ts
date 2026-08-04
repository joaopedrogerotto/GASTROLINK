import { PedidoHub } from "./Hubs/pedidoHub.js";

import * as GerarPagamento from "./gerarPagamento.js"

import { Pedido } from "./Models/pagamentoModel.js";


const hub = new PedidoHub("pedidoHub");

await hub.startConnection();

hub.onPedidoAguarndandoPag((pedido: Pedido) => {
    adicionarNovoPedidoNaTela(pedido);
});

document.addEventListener("click", (e) => {
    const btnPedido = (e.target as HTMLElement).closest("#detalhesPedido") as HTMLElement | null;

    if (!btnPedido) {
        return;
    }

    let idPedido = Number(btnPedido.dataset.id);

    $.ajax({
        url: '/Pedido/SelecionarPedidoPeloId?id=' + idPedido,
        method: 'GET',
        success: function (pedido) {
            montarModalDetalhes(pedido);
            const modal = document.getElementById("modalDetalhesPedido")!;

            if (modal) {
                bootstrap.Modal.getOrCreateInstance(modal).show();
            }
        },
        error: function (xhr, status, error) {
            console.log("Erro:" + error)
        }
    });
});

document.addEventListener("click", (e) => {
    const btnPedido = (e.target as HTMLElement).closest("#pagamentoPedido") as HTMLElement | null;

    if (!btnPedido) {
        return;
    }

    let idPedido = Number(btnPedido.dataset.id);

    $.ajax({
        url: '/Pagamento/CarregarPagamento?id=' + idPedido,
        method: 'GET',
        success: function (dadosPagamento) {
            GerarPagamento.montarModalPagamento(dadosPagamento);
            const modal = document.getElementById("modalDetalhesPedido")!;

            if (modal) {
                bootstrap.Modal.getOrCreateInstance(modal).show();
            }
        },
        error: function (xhr, status, error) {
            console.log("Erro:" + error)
        }
    });
});


function montarModalDetalhes(pedido: Pedido) {
    const tituloModal = document.getElementById("tituloPedido")!;
    const conteudo = document.getElementById("conteudoPedido")!;

    tituloModal.innerHTML = `
    <div class="d-flex justify-content-between align-items-center">
        <span class="fw-bold">
            Pedido #${pedido.id}
        </span>

        <small class="text-muted">
            ${new Date(pedido.dataCriacao).toLocaleString("pt-BR", {
        day: "2-digit",
        month: "2-digit",
        year: "numeric",
        hour: "2-digit",
        minute: "2-digit"
    })}
        </small>
    </div>
`;

    conteudo.innerHTML = `
        <div class="card border-0 shadow-sm">

            <div class="card-body">

                <div class="row mb-4">

                    <div class="col-md-6">
                        <small class="text-muted">Mesa</small>
                        <h5>${pedido.mesa.numero}</h5>
                    </div>

                    <div class="col-md-6">
                        <small class="text-muted">Garçom</small>
                        <h5>${pedido.usuario.nome}</h5>
                    </div>

                </div>

                <h6 class="border-bottom pb-2 mb-3">
                    Itens do Pedido
                </h6>

                ${pedido.itens.map(item => `
                    <div class="d-flex justify-content-between align-items-start border rounded p-3 mb-2">

                        <div>
                            <strong>${item.quantidade}x ${item.prato.nome}</strong>

                            ${item.observacao ? `<div class="text-muted small">${item.observacao}</div>` : ""}
                        </div>

                        <span class="badge bg-primary rounded-pill">
                            ${item.quantidade}x
                        </span>

                    </div>
                `).join("")}

                <div class="text-end">
                    <div>${pedido.valorTotal.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL"
    })}</div>
                </div>

            </div>

        </div>
    `;
}
function adicionarNovoPedidoNaTela(pedido: Pedido): void {
    const container = document.getElementById("containerPedidos");

    container?.insertAdjacentHTML(
        "beforeend",
        `<div class="card" style="width: 18rem;" id="pedido-${pedido.id}">
            <div class="card-body">
                <h5 class="card-title">Pedido ${pedido.id}</h5>
                <h6 class="card-subtitle mb-2 text-muted">Mesa ${pedido.mesa.numero}</h6>
                <p class="card-text">Quantidade de itens: ${pedido.itens.length}</p>
                <a href="#" class="card-link" id="detalhesPedido" data-id="${pedido.id}">Visualizar</a>
                <a href="#" class="card-link" id="pagamentoPedido" data-id="${pedido.id}">Pagamento</a>
            </div>
        </div>`
    );
}