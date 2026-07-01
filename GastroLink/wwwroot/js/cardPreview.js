"use strict";
const nome = document.getElementById("prt_nome");
const descricao = document.getElementById("prt_descricao");
const preco = document.getElementById("prt_preco");
const tempoMedio = document.getElementById("prt_tempo");
const selectCategoria = document.getElementById("prt_categoria");
function atualizarTitulo() {
    const nomePrato = nome.value || "Nome do prato";
    const nomeCategoria = selectCategoria.selectedOptions[0]?.text || "Categoria";
    document.getElementById("previewNome").textContent = `${nomePrato} - ${nomeCategoria}`;
}
function atualizarDescricao() {
    document.getElementById("previewDescricao").textContent = descricao.value || "Descrição do prato";
}
function atualizarPreco() {
    const valor = parseFloat(preco.value);
    document.getElementById("previewPreco").textContent = `R$ ${isNaN(valor) ? "0,00" : valor.toFixed(2).replace(".", ",")}`;
}
function atualizarTempoMedio() {
    document.getElementById("previewTempo").textContent = `Tempo médio de preparo: ${tempoMedio.value || "0"} min`;
}
nome.addEventListener("input", atualizarTitulo);
selectCategoria.addEventListener("change", atualizarTitulo);
descricao.addEventListener("input", atualizarDescricao);
preco.addEventListener("input", atualizarPreco);
tempoMedio.addEventListener("input", atualizarTempoMedio);
atualizarTitulo();
atualizarDescricao();
atualizarPreco();
atualizarTempoMedio();
const inputImagem = document.getElementById("formFile");
const preview = document.getElementById("previewImagem");
inputImagem.addEventListener("change", () => {
    const arquivo = inputImagem.files?.[0];
    if (!arquivo) {
        preview.src = "";
        preview.style.display = "none";
        return;
    }
    preview.src = URL.createObjectURL(arquivo);
    preview.classList.remove("d-none");
});
//# sourceMappingURL=cardPreview.js.map