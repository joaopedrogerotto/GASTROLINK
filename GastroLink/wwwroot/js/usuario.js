import { recarregarTabelaUsuarios } from "./tabelaUsuario.js";
export function SalvarUsuario() {
    const idInput = document.getElementById("usu_id");
    const nomeInput = document.getElementById("usu_nome");
    const loginInput = document.getElementById("usu_login");
    const tipoUsuarioInput = document.getElementById("usu_tipo_usu_id");
    const usuario = {
        Id: parseInt(idInput.value),
        Nome: nomeInput.value,
        Login: loginInput.value,
        TipoUsuarioId: parseInt(tipoUsuarioInput.value)
    };
    $.ajax({
        url: '/Usuario/AtualizarUsuario',
        method: 'PUT',
        contentType: 'application/json',
        data: JSON.stringify(usuario),
        success: function (response) {
            $("#textSucessoUsuario").text("Usuário atualizado com sucesso.");
            $("#tituloSucessoUsuario").text("Atualização Usuário");
            const modalElementShow = document.getElementById("modalSucessoUsuario");
            if (modalElementShow) {
                bootstrap.Modal
                    .getOrCreateInstance(modalElementShow)
                    .show();
            }
            recarregarTabelaUsuarios();
            const modalElementHide = document.getElementById("visualizarUsuarioModal");
            if (modalElementHide) {
                bootstrap.Modal
                    .getOrCreateInstance(modalElementHide)
                    .hide();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao salvar o usuário:", error);
        }
    });
}
//# sourceMappingURL=usuario.js.map