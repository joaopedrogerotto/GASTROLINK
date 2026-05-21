function SalvarUsuario(){
    const usuario = {
        Id: document.getElementById("usu_id").value,
        Nome: document.getElementById("usu_nome").value,
        Login: document.getElementById("usu_login").value,
        TipoUsuarioId: document.getElementById("usu_tipo_usu_id").value
    }

    $.ajax({
        url: 'https://localhost:7209/api-gastrolink/Usuario',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            $("#textSucessoUsuario").text("Usuário atualizado com sucesso.");
            $("#tituloSucessoUsuario").text("Atualização Usuário");
            $("#modalSucessoUsuario").modal('show');
            recarregarTabelaUsuarios();
            $("#visualizarUsuarioModal").modal('hide');
        },
        error: function (xhr, status, error) {
            console.error("Erro ao salvar o usuário:", error);
        }
    })
}