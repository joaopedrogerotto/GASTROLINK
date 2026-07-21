import { atualizarQuantidadeItensPedido } from "./cardapio.js";

document.addEventListener("click", (e) => {
    const btnConfirmarPedido = (e.target as HTMLElement).closest("#btnConfirmarPedido");

    if (!btnConfirmarPedido) {
        return;
    }

    const mesaId = (document.getElementById("mesaId") as HTMLInputElement).value;

    $.ajax({
        url: '/Pedido/GerarPedido',
        method: 'POST',
        data: JSON.stringify(Number(mesaId)),
        contentType: 'application/json',
        success: function () {
            const modalResumo = document.getElementById("modalResumoPrato");
            if (modalResumo) {
                bootstrap.Modal.getOrCreateInstance(modalResumo).hide();
            }

            const modalSucessoCadastroPedido = document.getElementById("modalSucessoCadastroPedido");
            if (modalSucessoCadastroPedido) {
                bootstrap.Modal.getOrCreateInstance(modalSucessoCadastroPedido).show();
            } 

            atualizarQuantidadeItensPedido();
        },
        error: function (xhr, status, error) {
            const modalErroCadastroPedido = document.getElementById("modalFalhaCadastroPedido");
            if (modalErroCadastroPedido) {
                const txtErro = document.getElementById("textFalhaCadastroPedido");
                if (txtErro) {
                    txtErro.textContent = "Falha ao criar pedido:" + error;
                }
                bootstrap.Modal.getOrCreateInstance(modalErroCadastroPedido).show();
            }
        }
    })
});

document.getElementById("okModalSucessoCadPdd")?.addEventListener("click", () => {
    window.location.href = '/Mesa/TodasMesas'
})