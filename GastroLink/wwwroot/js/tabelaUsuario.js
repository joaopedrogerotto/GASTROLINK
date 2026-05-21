function visualizarUsuario(idUsuario, acao) {
    $.ajax({
        url: '/Usuario/VisualizarUsuario?idUsuario=' + idUsuario,
        method: 'GET',
        success: function (html) {
            $("#modalContainer").html(html);


            const inputs = $("#visualizarUsuarioModal input");

            if (acao === "info") {
                inputs.prop("readonly", true);
                inputs.addClass("input-readonly");
            } else {
                inputs.prop("readonly", false);
                inputs.removeClass("input-readonly");

                const btnSalvar = $("#btnAcaoModal");
                btnSalvar.text("Salvar").removeAttr("data-bs-dismiss");
                btnSalvar.off("click").on("click", function () {
                    SalvarUsuario();
                });
            }

            $("#visualizarUsuarioModal").modal('show');
        },
        error: function (xhr, status, error) {
            console.error("Erro ao carregar os dados do usuário:", error);
        }
    })
}

function recarregarTabelaUsuarios() {
    $("#visualizarUsuarioModal").modal('hide');

    $("#tabelaTodosUsuarios").load("/Usuario/TabelaUsuario");
}