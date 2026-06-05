interface CategoriaPrato {
    Categoria: string;
}

async function criarCategoriaPrato(): Promise<void> {
    const categoriaInput = $("#categoriaPratoInput").val() as string;

    const categoriaPrato: CategoriaPrato = { Categoria: categoriaInput };

    const modalCadastroCategoria = document.getElementById("modalCategoriaPrato");
    const response = await fetch('https://localhost:7209/api-gastrolink/CategoriaPrato', {
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
        } else {
            const erro = await response.json();

            $("#statusCadastroCategoria").text(erro.message ?? erro.Message ?? "Erro desconhecido.");
        }
        bootstrap.Modal.getOrCreateInstance(modalCadastroCategoria).show();
        $("#categoriaPratoInput").val("");
    }
}