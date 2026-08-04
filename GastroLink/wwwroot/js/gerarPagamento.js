export function montarModalPagamento(dadosPagamento) {
    const pedido = dadosPagamento.pedido;
    const formasPagamento = dadosPagamento.formasPagamento;
    const tituloModal = document.getElementById("tituloPedido");
    const conteudo = document.getElementById("conteudoPedido");
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

                <hr>

                <div class="mb-3">
                    <label for="formaPagamento" class="form-label">
                        Forma de pagamento
                    </label>

                    <select class="form-select" id="formaPagamento">
                        <option value="">Selecione...</option>

                        ${formasPagamento.map(fp => `
                            <option value="${fp.id}">
                                ${fp.forma}
                            </option>
                        `).join("")}
                    </select>
                </div>


                <div class="mb-3">
                    <label for="desconto" class="form-label">
                        Desconto (R$)
                    </label>

                    <input type="number" class="form-control" id="desconto" min="0" max="${pedido.valorTotal}" step="0.01" value="0">
                </div>

                <div class="text-end">
                    <small class="text-muted d-block">Total</small>

                    <h4 class="fw-bold" id="valorTotal">
                        ${pedido.valorTotal.toLocaleString("pt-BR", {
        style: "currency",
        currency: "BRL"
    })}
                    </h4>
                </div>

                <div class="text-end">
                    <button type="button" class="btn btn-success" id="btnGerarPagamento">Gerar Pagamento</button>
                </div>

            </div>

        </div>
    `;
    const inputDesconto = document.getElementById("desconto");
    const total = document.getElementById("valorTotal");
    inputDesconto.addEventListener("input", () => {
        let desconto = parseFloat(inputDesconto.value);
        if (isNaN(desconto)) {
            desconto = 0;
        }
        if (desconto < 0) {
            desconto = 0;
        }
        if (desconto > pedido.valorTotal) {
            desconto = pedido.valorTotal;
            inputDesconto.value = desconto.toString();
        }
        const valorFinal = pedido.valorTotal - desconto;
        total.textContent = valorFinal.toLocaleString("pt-BR", {
            style: "currency",
            currency: "BRL"
        });
    });
    const btnGerarPagamento = document.getElementById("btnGerarPagamento");
    let idOrderGateway = "GATEWAY_ID";
    let intervaloVerificacao;
    btnGerarPagamento.addEventListener("click", () => {
        const formaPagamentoSelect = document.getElementById("formaPagamento");
        const IdFormaPagamento = Number(formaPagamentoSelect.value);
        const desconto = Number(inputDesconto.value);
        const valorTotal = pedido.valorTotal;
        const valorPago = valorTotal - desconto;
        const pagamento = {
            Desconto: desconto,
            ValorPago: valorPago,
            ValorTotal: valorTotal,
            IdPedido: pedido.id,
            IdFormaPagamento: IdFormaPagamento,
            IdUsuario: 0
        };
        if (IdFormaPagamento == 4) {
            GerarQrCodePix(pagamento);
        }
        else {
            registrarPagamento(pagamento);
        }
        function GerarQrCodePix(pagamento) {
            $.ajax({
                url: '/Pagamento/GerarQrCodePix',
                method: 'POST',
                data: JSON.stringify(pagamento),
                contentType: 'application/json',
                success: function (response) {
                    const img = document.getElementById("imgQrCode");
                    img.src = `data:image/png;base64,${response.qrCodeBase64}`;
                    const txtPix = document.getElementById("txtPix");
                    txtPix.value = response.codigoPix;
                    const modal = document.getElementById("modalQrCodePix");
                    idOrderGateway = response.idOrderMercadoPago;
                    bootstrap.Modal.getOrCreateInstance(modal).show();
                    iniciarVerificacaoPagamento(pedido.id, idOrderGateway, valorPago);
                },
                error: function (xhr, status, error) {
                    const modalErro = document.getElementById("modalFalhaPagamento");
                    if (modalErro) {
                        const mensagemErro = document.getElementById("txtFalhaPag");
                        let mensagem = "Ocorreu um erro ao processar o pagamento.";
                        if (xhr.responseText) {
                            try {
                                const erro = JSON.parse(xhr.responseText);
                                mensagem = erro.msg ?? mensagem;
                            }
                            catch {
                                mensagem = xhr.responseText;
                            }
                        }
                        mensagemErro.textContent = mensagem;
                        bootstrap.Modal.getOrCreateInstance(modalErro).show();
                    }
                    console.log("Erro:" + error);
                }
            });
        }
        function registrarPagamento(pagamento) {
            $.ajax({
                url: '/Pagamento/RegistrarPagamento',
                method: 'POST',
                data: JSON.stringify(pagamento),
                contentType: 'application/json',
                success: function (response) {
                    const modalDetalhes = document.getElementById("modalDetalhesPedido");
                    if (modalDetalhes) {
                        bootstrap.Modal.getOrCreateInstance(modalDetalhes).hide();
                    }
                    const modal = document.getElementById("modalSucessoPagamento");
                    if (modal) {
                        bootstrap.Modal.getOrCreateInstance(modal).show();
                    }
                    const cardPedido = document.getElementById(`pedido-${pedido.id}`);
                    if (cardPedido) {
                        cardPedido.remove();
                    }
                },
                error: function (xhr, status, error) {
                    const modalErro = document.getElementById("modalFalhaPagamento");
                    if (modalErro) {
                        const mensagemErro = document.getElementById("txtFalhaPag");
                        let mensagem = "Ocorreu um erro ao processar o pagamento.";
                        if (xhr.responseText) {
                            try {
                                const erro = JSON.parse(xhr.responseText);
                                mensagem = erro.msg ?? mensagem;
                            }
                            catch {
                                mensagem = xhr.responseText;
                            }
                        }
                        mensagemErro.textContent = mensagem;
                        bootstrap.Modal.getOrCreateInstance(modalErro).show();
                    }
                    console.log("Erro:" + error);
                }
            });
        }
        function iniciarVerificacaoPagamento(idPedido, idOrderMercadoPago, valorPago) {
            if (intervaloVerificacao) {
                clearInterval(intervaloVerificacao);
            }
            intervaloVerificacao = window.setInterval(() => {
                $.ajax({
                    url: '/Pagamento/VerificarQrCode',
                    method: 'POST',
                    data: JSON.stringify({
                        IdPedido: idPedido,
                        IdOrderMercadoPago: idOrderMercadoPago,
                        valorPago: valorPago
                    }),
                    contentType: 'application/json',
                    success: function (data) {
                        if (data === 1) {
                            clearInterval(intervaloVerificacao);
                            intervaloVerificacao = undefined;
                            const modal = document.getElementById("modalQrCodePix");
                            bootstrap.Modal.getOrCreateInstance(modal).hide();
                            registrarPagamento(pagamento);
                        }
                    },
                    error: function (xhr, status, error) {
                        console.log("Erro ao verificar pagamento:", error);
                    }
                });
            }, 20000);
        }
        const modalQrCode = document.getElementById("modalQrCodePix");
        if (modalQrCode) {
            modalQrCode.addEventListener("hidden.bs.modal", () => {
                if (intervaloVerificacao) {
                    clearInterval(intervaloVerificacao);
                    intervaloVerificacao = undefined;
                }
            });
        }
    });
}
//# sourceMappingURL=gerarPagamento.js.map