"use strict";
;
document.querySelectorAll(".card-prato-link").forEach(card => {
    card.addEventListener("click", () => {
        const id = card.getAttribute("data-id");
        visualizarPrato(Number(id));
    });
});
function visualizarPrato(idUsuario) {
    $.ajax({
        url: '/Prato/VisualizarPrato?idPrato=' + idUsuario,
        method: 'GET',
        success: function (html) {
            $("#modalContainerPrato").html(html);
            const modalElement = document.getElementById("visualizarPratoModel");
            if (modalElement) {
                bootstrap.Modal.getOrCreateInstance(modalElement).show();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao carregar os dados do prato: ", error);
        }
    });
}
document.querySelectorAll(".alterar-status-prato").forEach(card => {
    card.addEventListener("click", (e) => {
        e.preventDefault();
        e.stopPropagation();
        const elemento = card;
        const id = Number(elemento.dataset.idPrato);
        const status = elemento.dataset.status?.toLowerCase() === "true";
        console.log({ id, status });
        atualizarDisponibilidadePrato(id, status);
    });
});
function atualizarDisponibilidadePrato(id, status) {
    const PratoStatus = { Id: id, Status: !status };
    $.ajax({
        url: "/Prato/AtualizarDisponibilidade",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(PratoStatus),
        success: function () {
            console.log("Disponibilidade atualizada.");
            location.reload();
        },
        error: function (xhr) {
            console.error(xhr.responseText);
        }
    });
}
//# sourceMappingURL=prato.js.map