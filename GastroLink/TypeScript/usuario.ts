interface Usuario {
    Id: number;
    Nome: string;
    Login: string;
    TipoUsuarioId: number;
}

import { recarregarTabelaUsuarios } from "./tabelaUsuario.js";

export function SalvarUsuario(): void{
    const idInput = document.getElementById("usu_id") as HTMLInputElement;
    const nomeInput = document.getElementById("usu_nome") as HTMLInputElement;
    const loginInput = document.getElementById("usu_login") as HTMLInputElement;
    const tipoUsuarioInput = document.getElementById("usu_tipo_usu_id") as HTMLInputElement;

    const usuario: Usuario = {
        Id: parseInt(idInput.value),
        Nome: nomeInput.value,
        Login: loginInput.value,
        TipoUsuarioId: parseInt(tipoUsuarioInput.value)
    }

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
        error: function (xhr: JQueryXHR, status: string, error: string): void {
            console.error("Erro ao salvar o usuário:", error);
        }
    })
}