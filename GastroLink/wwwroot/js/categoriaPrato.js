"use strict";
async function criarCategoriaPrato() {
    const categoriaInput = $("#categoriaPratoInput").val();
    const categoriaPrato = { Categoria: categoriaInput };
    const modalCadastroCategoria = document.getElementById("modalCategoriaPrato");
    const response = await fetch('https://localhost:7209/api-gastrolink/CategoriaPrato', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(categoriaPrato)
    });
    if (response.ok) {
        $("#statusCadastroCategoria").text("Categoria de prato criada com sucesso.");
        bootstrap.Modal.getOrCreateInstance(modalCadastroCategoria).show();
        $("#categoriaPratoInput").val("");
    }
    else {
        $("#statusCadastroCategoria").text("Erro ao criar categoria de prato: " + response.statusText);
        bootstrap.Modal.getOrCreateInstance(modalCadastroCategoria).hide();
        $("#categoriaPratoInput").val("");
    }
}
//# sourceMappingURL=categoriaPrato.js.map