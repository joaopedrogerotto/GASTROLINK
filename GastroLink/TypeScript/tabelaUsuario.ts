type AcaoUsuario = "info" | "editar" | "ativo" | "inativo";

import { SalvarUsuario } from "./usuario.js";

function visualizarUsuario(idUsuario: number, acao: AcaoUsuario): void {
    $.ajax({
        url: '/Usuario/VisualizarUsuario?idUsuario=' + idUsuario,
        method: 'GET',
        success: function (html: string) {
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
                btnSalvar.off("click").on("click", function (): void{
                    SalvarUsuario();
                });
            } else {
                inputs.prop("readonly", true);
                inputs.addClass("input-readonly");

                const btnExcluir = $("#btnAcaoModal");

                if (acao === 'ativo') {
                    btnExcluir.text("Inativar").removeAttr("data-bs-dismiss");
                    btnExcluir.off("click").on("click", function (): void {
                        alterarStatusUsuario(idUsuario, false);
                    });
                } else {
                    btnExcluir.text("Ativar").removeAttr("data-bs-dismiss");
                    btnExcluir.off("click").on("click", function (): void {
                        alterarStatusUsuario(idUsuario, true);
                    });
                }
            }

            const modalElement = document.getElementById("visualizarUsuarioModal");

            if (modalElement) {
                bootstrap.Modal
                    .getOrCreateInstance(modalElement)
                    .show();
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao carregar os dados do usuário:", error);
        }
    })
}

export function recarregarTabelaUsuarios() {
    const modalElement = document.getElementById("visualizarUsuarioModal");

    if (modalElement) {
        bootstrap.Modal
            .getOrCreateInstance(modalElement)
            .hide();
    }

    $("#tabelaTodosUsuarios").load("/Usuario/TabelaUsuario");
}

function alterarStatusUsuario(idUsuario: number, status: boolean) : void {
    const usuario = {
        Id: idUsuario,
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
            const modalElement = document.getElementById("modalSucessoUsuario");

            if (modalElement) {
                bootstrap.Modal
                    .getOrCreateInstance(modalElement)
                    .show();
            }
            recarregarTabelaUsuarios();
        }, error: function (xhr, status, error) {
            console.error("Erro ao excluir o usuário:", error);
        }
    })
}

document.addEventListener("click", (e) => {
    const target = e.target as HTMLElement;

    const btn = target.closest("#info-usuario");

    if (!btn) return;

    const idUsuario = Number(btn.getAttribute("data-id"));

    visualizarUsuario(idUsuario, "info");
});

document.addEventListener("click", (e) => {
    const target = e.target as HTMLElement;

    const btn = target.closest("#inativar-usuario");

    if (!btn) return;

    const idUsuario = Number(btn.getAttribute("data-id"));

    visualizarUsuario(idUsuario, "inativo");
});

document.addEventListener("click", (e) => {
    const target = e.target as HTMLElement;

    const btn = target.closest("#ativar-usuario");

    if (!btn) return;

    const idUsuario = Number(btn.getAttribute("data-id"));

    visualizarUsuario(idUsuario, "ativo");
});

document.addEventListener("click", (e) => {
    const target = e.target as HTMLElement;

    const btn = target.closest("#editar-usuario");

    if (!btn) return;

    const idUsuario = Number(btn.getAttribute("data-id"));

    visualizarUsuario(idUsuario, "editar");
});