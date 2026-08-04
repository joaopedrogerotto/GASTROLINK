import { CategoriaPrato } from "./Models/categoriaPratoModel";

async function criarCategoriaPrato(): Promise<void> {
    const categoriaInput = $("#categoriaPratoInput").val() as string;

    const categoriaPrato: CategoriaPrato = { id: 0, categoria: categoriaInput };

    const modalCadastroCategoria = document.getElementById("modalCategoriaPrato");
    const response = await fetch('/CategoriaPrato/CadastrarCategoria', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(categoriaPrato)
    });


    const header = $("#headerModalCadastroCategoria");
    if (response.ok) {
        header.removeClass("bg-danger text-white");
        header.addClass("bg-success text-white");
        $("#statusCadastroCategoria").text("Categoria de prato criada com sucesso.");
        bootstrap.Modal.getOrCreateInstance(modalCadastroCategoria).show();
        $("#categoriaPratoInput").val("");
    } else {
        header.removeClass("bg-success text-white");
        header.addClass("bg-danger text-white");
        if (response.status === 500) {
            $("#statusCadastroCategoria").text("Erro interno do servidor.");
        } else if (response.status === 409) { 
            $("#statusCadastroCategoria").text("Categoria já cadastrada");
        } else {
            const erro = await response.json();

            $("#statusCadastroCategoria").text(erro.message ?? erro.Message ?? "Erro desconhecido.");
        }
        bootstrap.Modal.getOrCreateInstance(modalCadastroCategoria).show();
        $("#categoriaPratoInput").val("");
    }
    carregarCategoriasPrato();
}

async function carregarCategoriasPrato(): Promise<void> {
    const response = await fetch('/CategoriaPrato/TodasCategoriasJson');
    const categorias: CategoriaPrato[] = await response.json();

    const tbody = document.querySelector("#tabelaCategoriasPrato tbody") as HTMLTableSectionElement;
    tbody.innerHTML = "";

    categorias.forEach((categoria: CategoriaPrato) => {
        const linha = `
            <tr>
                <td>${categoria.categoria}</td>
            </tr>
            `;
        tbody.innerHTML += linha;
    });
}

document.addEventListener("DOMContentLoaded", () => {
    carregarCategoriasPrato();
});