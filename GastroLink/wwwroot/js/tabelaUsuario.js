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
            } else if (acao === "editar") {
                inputs.prop("readonly", false);
                inputs.removeClass("input-readonly");

                const btnSalvar = $("#btnAcaoModal");
                btnSalvar.text("Salvar").removeAttr("data-bs-dismiss");
                btnSalvar.off("click").on("click", function () {
                    SalvarUsuario();
                });
            } else {
                inputs.prop("readonly", true);
                inputs.addClass("input-readonly");

                const btnExcluir = $("#btnAcaoModal");
                btnExcluir.text("Inativar").removeAttr("data-bs-dismiss");
                btnExcluir.off("click").on("click", function () {
                    excluirUsuario(idUsuario);
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

function excluirUsuario(idUsuario) {
    const usuario = {
        Id: parseInt(idUsuario),
        Status: false
    };

    $.ajax({
        url: 'https://localhost:7209/api-gastrolink/Usuario',
        method: 'DELETE',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            $("#textSucessoUsuario").text("Usuário inativado com sucesso.");
            $("#tituloSucessoUsuario").text("Inativação Usuário");
            $("#modalSucessoUsuario").modal('show');
            recarregarTabelaUsuarios();
        }, error: function (xhr, status, error) {
            console.error("Erro ao excluir o usuário:", error);
        }
    })
}