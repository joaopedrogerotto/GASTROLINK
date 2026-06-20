"use strict";
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
//# sourceMappingURL=prato.js.map