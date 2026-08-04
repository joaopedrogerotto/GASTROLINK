import { PratoStatus } from "./Models/pratoModel.js";
import { FiltroBusca } from "./Models/filtroBuscaModel.js";

document.addEventListener("click", (e) => {
    const alterarStatus = (e.target as HTMLElement).closest(".alterar-status-prato");

    if (alterarStatus) {
        const id = Number(alterarStatus.getAttribute("data-id-prato"));
        visualizarPrato(id, alterarStatus as HTMLElement);
        return;
    }

    const card = (e.target as HTMLElement).closest(".card-prato-link");

    if (card) {
        const id = Number(card.getAttribute("data-id"));
        visualizarPrato(id, card as HTMLElement);
    }
});

function visualizarPrato(idUsuario: number, target: HTMLElement): void {
    $.ajax({
        url: '/Prato/VisualizarPrato?idPrato=' + idUsuario,
        method: 'GET',
        success: function (html: string) {
            $("#modalContainerPrato").html(html);
            const modalElement = document.getElementById("visualizarPratoModel");
            if (modalElement) {
                bootstrap.Modal.getOrCreateInstance(modalElement).show();
            }

            if (target.classList.contains("alterar-status-prato")) {
                const container = document.getElementById("justificativaContainer");

                if (container) {
                    container.innerHTML = `
                            <hr>
                            <div class="mb-3">
                                <label for="txtJustificativa" class="form-label">
                                    Justificativa da alteração
                                </label>
                                <textarea
                                    id="txtJustificativa"
                                    class="form-control"
                                    rows="3"
                                    placeholder="Digite o motivo da alteração de disponibilidade..."
                                ></textarea>
                            </div>
                        `;
                }

                const footer = document.getElementById("modalFooterPrato");

                if (footer && !document.getElementById("btnAlterarDisponibilidade")) {
                    const botao = document.createElement("button");

                    botao.type = "button";
                    botao.id = "btnAlterarDisponibilidade";
                    botao.className = "btn btn-danger";
                    botao.textContent = "Atualizar Disponibilidade";
                    botao.dataset.idPrato = idUsuario.toString();
                    botao.dataset.status = target.getAttribute("data-status") ?? "false";

                    footer.appendChild(botao);
                }
            } else {
                const container = document.getElementById("justificativaContainer");

                if (container) {
                    container.innerHTML = "";
                }

                const botao = document.getElementById("btnAlterarDisponibilidade");

                if (botao) {
                    botao.remove();
                }
            }
        },
        error: function (xhr, status, error) {
            console.error("Erro ao carregar os dados do prato: ", error);
        }
    })
}

document.addEventListener("click", (e) => {
    const btn = (e.target as HTMLElement).closest("#btnAlterarDisponibilidade");

    if (!btn) return;

    e.preventDefault();
    e.stopPropagation();

    const id = Number(btn.getAttribute("data-id-prato"));
    const status = btn.getAttribute("data-status")?.toLowerCase() === "true";
    const justificativaInput = document.getElementById("txtJustificativa") as HTMLTextAreaElement | null;       

    atualizarDisponibilidadePrato(id, status, justificativaInput?.value || "");
});

function atualizarDisponibilidadePrato(id: Number, status: boolean, justificativa: string): void {
    const PratoStatus: PratoStatus = { Id: id, Status: !status, Justificativa: justificativa, IdUsuario: 0 };

    $.ajax({
        url: "/Prato/AtualizarDisponibilidade",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(PratoStatus),
        success: function () {
            location.reload();
        },
        error: function (xhr) {
            console.error(xhr.responseText);
        }
    })
}

function carregarPratos(): void {
    $.ajax({
        url: "/Prato/ListaPratos",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(null),
        success: function (response) {
            document.getElementById("lista-pratos")!.innerHTML = response;
        }
    })
}

function obterFiltros(): FiltroBusca {
    const filtro: Partial<FiltroBusca> = {};

    document.querySelectorAll("#list-filtro span")?.forEach(badge => {
        const elemento = badge as HTMLElement;

        const categoria = elemento.dataset.categoria;
        const texto = elemento.textContent!.replace("x", "").trim();

        const valor = texto.split(":")[1].trim();

        switch (categoria) {
            case "Nome":
                filtro.Nome = valor;
                break;

            case "Descrição":
                filtro.Descricao = valor;
                break;

            case "Preço":
                filtro.Preco = parseFloat(valor.replace(",", "."));
                break;
        }
    });

    const selectPrato = document.getElementById("select-cat-prato") as HTMLSelectElement;
    if (selectPrato.value !== "") {
        filtro.IdCategoria = Number(selectPrato.value);
    }

    const selectDisponibilidade = document.getElementById("select-disponibilidade") as HTMLSelectElement;
    if (selectDisponibilidade.value !== "") {
        filtro.Disponibilidade = Number(selectDisponibilidade.value) === 1 ? true : false;
    }

    return filtro as FiltroBusca;
}

function pesquisarPrato(): void{
    const filtro = obterFiltros();

    $.ajax({
        url: "/Prato/ListaPratos",
        method: "POST",
        contentType: 'application/json',
        data: JSON.stringify(filtro),
        success: function (response) {
            document.getElementById("lista-pratos")!.innerHTML = '';
            document.getElementById("lista-pratos")!.innerHTML = response;
        }
    })
}

document.addEventListener("DOMContentLoaded", () => {
    carregarPratos();
});

document.getElementById("btn-pesquisa-prato")!.addEventListener("click", () => {
    pesquisarPrato();
})

const button = document.getElementById("titulo-opcoes") as HTMLButtonElement;
const input = document.getElementById("input-pesquisa") as HTMLInputElement;
const opcoesPesquisa = document.getElementById("opcoes-pesquisa") as HTMLUListElement;

const opcoesRemovidas = new Map<string, HTMLLIElement>();

opcoesPesquisa.addEventListener("click", (e) => {
    const target = e.target as HTMLElement;

    if (!target.classList.contains("opcao-filtro")) {
        return;
    }

    e.preventDefault();

    const texto = target.dataset.valor!;
    button.textContent = texto;

    if (texto === "Preço") {
        input.type = "number";
        input.step = "0.01";
        input.min = "0";
    } else {
        input.type = "text";
        input.removeAttribute("step");
        input.removeAttribute("min");
    }
});

document.getElementById("btn-add-filtro")?.addEventListener("click", () => {
    const container = document.getElementById("list-filtro") as HTMLElement;

    const categoria = button.textContent!;
    const valor = input.value.trim();

    if (categoria === "Categorias" || valor === "") {
        return;
    }

    const badge = document.createElement("span");
    badge.classList.add("badge", "bg-primary", "me-2");
    badge.dataset.categoria = categoria;
    badge.textContent = `${categoria}: ${valor} `;

    const btnRemover = document.createElement("i");
    btnRemover.classList.add("fas", "fa-times", "ms-2");
    btnRemover.style.cursor = "pointer";

    badge.appendChild(btnRemover);
    container.appendChild(badge);

    const li = document.querySelector(
        `[data-valor="${categoria}"]`
    )?.parentElement as HTMLLIElement | null;

    if (li) {
        opcoesRemovidas.set(categoria, li);
        li.remove();
    }

    btnRemover.addEventListener("click", () => {
        badge.remove();

        const opcao = opcoesRemovidas.get(categoria);

        if (opcao) {
            opcoesPesquisa.appendChild(opcao);
            opcoesRemovidas.delete(categoria);
        }
    });

    button.textContent = "Categorias";
    input.value = "";
    input.type = "text";
    input.removeAttribute("step");
    input.removeAttribute("min");
});

document.addEventListener("click", (e) => {
    const prato = (e.target as HTMLElement).closest(".editar-prato");

    if (!prato) return;

    e.preventDefault();
    e.stopPropagation();

    const id = Number(prato.getAttribute("data-id-prato"));
    window.location.href = `/Prato/EditarPrato?id=${id}`;
});