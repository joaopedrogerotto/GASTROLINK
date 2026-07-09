document.addEventListener("click", (e) => {
    const prato = e.target.closest(".add-prato-pedido");
    if (prato) {
        const id = Number(prato.getAttribute("data-id-prato"));
        visualizarPratoPedido(id, prato);
    }
});
function visualizarPratoPedido(idPrato, target) {
    $.ajax({
        url: '/Prato/VisualizarPrato?idPrato=' + idPrato,
        method: 'GET',
        success: function (html) {
            $("#modalContainerPrato").html(html);
            const modalElement = document.getElementById("visualizarPratoModel");
            if (modalElement) {
                const container = document.getElementById("observacaoPrato");
                if (container) {
                    container.innerHTML = `
                            <hr>
                            <div class="mb-3">
                                <label for="txtObservvacao" class="form-label">
                                    Observação do prato
                                </label>
                                <textarea
                                    id="txtObservacao"
                                    class="form-control"
                                    rows="3"
                                    placeholder="Digite a observação do prato..."
                                ></textarea>
                            </div>
                        `;
                }
                const footer = document.getElementById("modalFooterPrato");
                if (footer) {
                    const botao = document.createElement("button");
                    botao.type = "button";
                    botao.id = "btnAdicionarPrato";
                    botao.className = "btn btn-success";
                    botao.textContent = "Adicionar Prato";
                    botao.dataset.idPrato = idPrato.toString();
                    footer.appendChild(botao);
                }
                bootstrap.Modal.getOrCreateInstance(modalElement).show();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao carregar os dados do prato: ", error);
        }
    });
}
document.addEventListener("click", (e) => {
    const prato = e.target.closest("#btnAdicionarPrato");
    if (!prato) {
        return;
    }
    const mesaId = document.getElementById("mesaPedido").value;
    const observacao = document.getElementById("txtObservacao")?.value ?? "";
    const quantidade = document.getElementById("quantidadeItem").value ?? 1;
    const itemPedido = {
        Prato: { Id: Number(prato?.dataset.idPrato) },
        Observacao: observacao,
        Quantidade: Number(quantidade)
    };
    const rascunhoItemPedido = {
        mesaId: Number(mesaId),
        rascunhoItemPedido: itemPedido
    };
    if (prato) {
        $.ajax({
            url: '/Pedido/AdicionarItemRascunho',
            method: 'POST',
            data: JSON.stringify(rascunhoItemPedido),
            contentType: 'application/json',
            success: function () {
                const modalSucessoItemPedido = document.getElementById("modalSucessoItemPedido");
                if (modalSucessoItemPedido) {
                    bootstrap.Modal.getOrCreateInstance(modalSucessoItemPedido).show();
                }
                const modalPrato = document.getElementById("visualizarPratoModel");
                if (modalPrato) {
                    bootstrap.Modal.getOrCreateInstance(modalPrato).hide();
                }
                atualizarQuantidadeItensPedido();
            },
            error: function (xhr, status, error) {
                console.error("Erro ao adicionar item ao rascunho: ", error);
            }
        });
    }
});
export function atualizarQuantidadeItensPedido() {
    const mesaId = document.getElementById("mesaPedido").value;
    $.ajax({
        url: '/Pedido/ObterQuantidadeItensRascunhoPedido',
        method: 'GET',
        data: { idMesa: Number(mesaId) },
        contentType: 'application/json',
        success: function (quantidade) {
            document.getElementById("quantidadeItensPedido").textContent = quantidade.toString();
        },
        error: function (xhr, status, error) {
            console.error("Erro ao atualizar a quantidade de itens do pedido: ", error);
        }
    });
}
document.addEventListener("DOMContentLoaded", () => {
    atualizarQuantidadeItensPedido();
});
document.getElementById("btnResumoPedido")?.addEventListener("click", () => {
    const mesaId = document.getElementById("mesaPedido").value;
    $.ajax({
        url: '/Pedido/ResumoPedido',
        method: 'GET',
        data: { idMesa: Number(mesaId) },
        contentType: 'application/json',
        success: function (html) {
            $("#modalContainerResumo").html(html);
            const modalElement = document.getElementById("modalResumoPrato");
            if (modalElement) {
                bootstrap.Modal.getOrCreateInstance(modalElement).show();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao obter o resumo do pedido: ", error);
        }
    });
});
//# sourceMappingURL=cardapio.js.map