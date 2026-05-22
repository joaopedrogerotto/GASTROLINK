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

                if (acao === 'ativo') {
                    btnExcluir.text("Inativar").removeAttr("data-bs-dismiss");
                    btnExcluir.off("click").on("click", function () {
                        alterarStatusUsuario(idUsuario, false);
                    });
                } else {
                    btnExcluir.text("Ativar").removeAttr("data-bs-dismiss");
                    btnExcluir.off("click").on("click", function () {
                        alterarStatusUsuario(idUsuario, true);
                    });
                }
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

function alterarStatusUsuario(idUsuario,status) {
    const usuario = {
        Id: parseInt(idUsuario),
        Status: status
    };

    $.ajax({
        url: 'https://localhost:7209/api-gastrolink/Usuario/alterar-status',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            $("#textSucessoUsuario").text("Usuário atualizado com sucesso.");
            $("#tituloSucessoUsuario").text("Atualização Usuário");
            $("#modalSucessoUsuario").modal('show');
            recarregarTabelaUsuarios();
        }, error: function (xhr, status, error) {
            console.error("Erro ao excluir o usuário:", error);
        }
    })
}