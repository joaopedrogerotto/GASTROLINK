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

                    <div class="form-check form-switch">
                        <input class="form-check-input" type="checkbox" id="checkboxDividirConta">
                        <label class="form-check-label" for="checkboxDividirConta">Dividir pagamento</label>
                    </div>

                    <br/>

                    <div id="divFormaPagamento">
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

                <div class="text-end" id="divBtnPagamento">
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
    const pagamento = {
        Desconto: 0,
        ValorTotal: 0,
        IdPedido: pedido.id,
        IdUsuario: 0,
        Pagamentos: []
    };
    btnGerarPagamento.addEventListener("click", () => {
        const desconto = Number(inputDesconto.value);
        const formaPagamentoSelect = document.getElementById("formaPagamento");
        const valorTotal = Number(pedido.valorTotal);
        const valorPago = valorTotal - desconto;
        pagamento.Desconto = desconto;
        pagamento.ValorTotal = valorPago;
        pagamento.Pagamentos = construirPagamentos();
        if (verificaPagamentosPix()) {
            if (!checkboxDividirConta.checked && Number(formaPagamentoSelect.value) === 4) {
                const pagamentoPix = {
                    IdPedido: pedido.id,
                    ValorPagoPix: valorPago
                };
                GerarQrCodePix(pagamentoPix);
            }
            else {
                registrarPagamento(pagamento);
            }
        }
        else {
            modalErro("Aguarde a confirmação de todos os pagamentos via PIX antes de finalizar o pedido.");
        }
    });
    function GerarQrCodePix(pagamentoPix, row) {
        if (pagamentoPix.ValorPagoPix > pedido.valorTotal) {
            modalErro("O valor do pagamento via PIX não pode ser maior que o valor total do pedido.");
            return;
        }
        else if (checkboxDividirConta.checked && pagamentoPix.ValorPagoPix === pedido.valorTotal) {
            modalErro("O valor do pagamento via PIX não pode ser igual ao valor total do pedido quando a conta está dividida.");
            return;
        }
        $.ajax({
            url: '/Pagamento/GerarQrCodePix',
            method: 'POST',
            data: JSON.stringify(pagamentoPix),
            contentType: 'application/json',
            success: function (response) {
                const img = document.getElementById("imgQrCode");
                img.src = `data:image/png;base64,${response.qrCodeBase64}`;
                const txtPix = document.getElementById("txtPix");
                txtPix.value = response.codigoPix;
                const modal = document.getElementById("modalQrCodePix");
                idOrderGateway = response.idOrderMercadoPago;
                bootstrap.Modal.getOrCreateInstance(modal).show();
                iniciarVerificacaoPagamento(pedido.id, idOrderGateway, pagamentoPix.ValorPagoPix, row);
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
    function iniciarVerificacaoPagamento(idPedido, idOrderMercadoPago, valorPago, row) {
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
                        if (checkboxDividirConta.checked) {
                            const selectFormaPagamento = row?.querySelector("#formaPagamento");
                            const inputValor = row?.querySelector(".valor-pagamento");
                            const btnQrCode = row?.querySelector(".btn-qrcode");
                            selectFormaPagamento?.setAttribute("disabled", "true");
                            inputValor?.setAttribute("disabled", "true");
                            inputValor?.setAttribute("aria-readonly", "true");
                            btnQrCode?.setAttribute("disabled", "true");
                            const check = document.createElement("span");
                            check.classList.add("text-success", "ms-2");
                            check.innerHTML = `
                                <i class="bi bi-check-circle-fill"></i>
                                Pago
                            `;
                            inputValor.parentElement?.appendChild(check);
                            const valorPagoPix = Number(inputValor.value);
                            const modal = document.getElementById("modalQrCodePix");
                            bootstrap.Modal.getOrCreateInstance(modal).hide();
                        }
                        else {
                            clearInterval(intervaloVerificacao);
                            intervaloVerificacao = undefined;
                            const modal = document.getElementById("modalQrCodePix");
                            bootstrap.Modal.getOrCreateInstance(modal).hide();
                            registrarPagamento(pagamento);
                        }
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
    const checkboxDividirConta = document.getElementById("checkboxDividirConta");
    checkboxDividirConta.addEventListener("change", function () {
        const divFormaPagamento = document.getElementById("divFormaPagamento");
        const divBtnPagamento = document.getElementById("divBtnPagamento");
        if (this.checked) {
            divFormaPagamento.innerHTML = "";
            for (let i = 0; i <= 1; i++) {
                divFormaPagamento?.insertAdjacentHTML("beforeend", adicionarInputPagamento());
            }
            divBtnPagamento?.insertAdjacentHTML("afterbegin", `
                <button type="button" class="btn btn-primary" id="btnAddPagamento">
                    Adicionar pagamento
                </button>
            `);
        }
        else {
            divFormaPagamento.innerHTML = "";
            const btnAddPagamento = document.getElementById("btnAddPagamento");
            btnAddPagamento?.remove();
            divFormaPagamento?.insertAdjacentHTML("beforeend", `
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
            `);
        }
        document.getElementById("btnAddPagamento")?.addEventListener("click", function () {
            const divFormaPagamento = document.getElementById("divFormaPagamento");
            divFormaPagamento?.insertAdjacentHTML("beforeend", adicionarInputPagamento());
        });
    });
    function adicionarInputPagamento() {
        return `
                <div class="row g-2">
                    <div class="col-md-6">
                        <label for="formaPagamento" class="form-label">
                            Forma de pagamento
                        </label>

                        <select class="form-select forma-pagamento" id="formaPagamento">
                            <option value="">Selecione...</option>
                            ${formasPagamento.map(fp => `
                                <option value="${fp.id}">
                                    ${fp.forma}
                                </option>
                            `).join("")}
                        </select>
                    </div>

                    <div class="col-md-6">
                        <label for="valorPagamento" class="form-label">
                            Valor
                        </label>


                        <div class="input-group">
                            <input type="number" class="form-control valor-pagamento" id="valorPagamento" step="0.01" min="0" placeholder="0,00">

                            <button  type="button" class="btn btn-outline-secondary btn-qrcode d-none" title="Gerar QR Code PIX"><i class="bi bi-qr-code"></i></button>
                        </div>
                    </div>
                </div>
                `;
    }
    function construirPagamentos() {
        let pagamentos = [];
        const divFormaPagamento = document.getElementById("divFormaPagamento");
        if (checkboxDividirConta.checked) {
            divFormaPagamento?.querySelectorAll(".row.g-2").forEach((row) => {
                let pagamento = {
                    IdFormaPagamento: Number(row.querySelector("#formaPagamento").value),
                    ValorPago: Number(row.querySelector("#valorPagamento").value)
                };
                pagamentos.push(pagamento);
            });
        }
        else {
            const formaPagamentoSelect = document.getElementById("formaPagamento");
            let pagamento = {
                IdFormaPagamento: Number(formaPagamentoSelect.value),
                ValorPago: Number(inputDesconto.value) > 0 ? pedido.valorTotal - Number(inputDesconto.value) : pedido.valorTotal
            };
            pagamentos.push(pagamento);
        }
        return pagamentos;
    }
    document.addEventListener("change", function (e) {
        const target = e.target;
        if (!target.classList.contains("forma-pagamento")) {
            return;
        }
        const rowPagamento = target.closest(".row");
        const btnQrCode = rowPagamento.querySelector(".btn-qrcode");
        if (!btnQrCode) {
            return;
        }
        if (Number(target.value) === 4) {
            btnQrCode.classList.remove("d-none");
        }
        else {
            btnQrCode.classList.add("d-none");
        }
    });
    document.addEventListener("click", function (e) {
        const target = e.target;
        const btnQrCode = target.closest(".btn-qrcode");
        if (!btnQrCode) {
            return;
        }
        const row = btnQrCode.closest(".row");
        const valor = Number(target.closest(".row").querySelector("#valorPagamento").value);
        const pagamentoPix = {
            IdPedido: pedido.id,
            ValorPagoPix: valor
        };
        GerarQrCodePix(pagamentoPix, row);
    });
    function verificaPagamentosPix() {
        const divFormaPagamento = document.getElementById("divFormaPagamento");
        const rows = divFormaPagamento?.querySelectorAll(".row.g-2");
        if (!rows || rows.length === 0) {
            return true;
        }
        return Array.from(rows).every(row => {
            const btnQrCode = row.querySelector(".btn-qrcode");
            if (!btnQrCode || btnQrCode.classList.contains("d-none")) {
                return true;
            }
            return row.querySelector(".bi-check-circle-fill") !== null;
        });
    }
    function modalErro(mensagem) {
        const modalErro = document.getElementById("modalFalhaPagamento");
        if (!modalErro) {
            return;
        }
        const mensagemErro = document.getElementById("txtFalhaPag");
        mensagemErro.textContent = mensagem;
        bootstrap.Modal.getOrCreateInstance(modalErro).show();
    }
    document.addEventListener("input", function (e) {
        const inputAtual = e.target;
        if (!inputAtual.classList.contains("valor-pagamento")) {
            return;
        }
        const inputs = [...document.querySelectorAll(".valor-pagamento")];
        const outrosInputs = inputs.filter(input => input !== inputAtual);
        const valorUsado = outrosInputs.reduce((total, input) => total + Number(input.value || 0), 0);
        const valorRestante = pedido.valorTotal - valorUsado;
        inputAtual.max = valorRestante.toString();
        if (Number(inputAtual.value) > valorRestante) {
            inputAtual.value = valorRestante.toString();
        }
    });
}
//# sourceMappingURL=gerarPagamento.js.map