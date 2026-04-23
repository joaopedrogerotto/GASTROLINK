let mesaSelecionada = null;
let offsetX = 0;
let offsetY = 0;

const mapa = document.getElementById("mapa");


document.querySelectorAll(".mesa").forEach(mesa => {

    mesa.addEventListener("mousedown", function (e) {

        mesaSelecionada = mesa;

        const rect = mesa.getBoundingClientRect();

        offsetX = e.clientX - rect.left;
        offsetY = e.clientY - rect.top;

        mesa.style.cursor = "grabbing";
    });

});


document.addEventListener("mousemove", function (e) {

    if (mesaSelecionada) {

        const mapaRect = mapa.getBoundingClientRect();

        const mesaWidth = mesaSelecionada.offsetWidth;
        const mesaHeight = mesaSelecionada.offsetHeight;

        let novaLeft = e.clientX - mapaRect.left - offsetX;
        let novaTop = e.clientY - mapaRect.top - offsetY;

        const maxX = mapa.clientWidth - mesaWidth;
        const maxY = mapa.clientHeight - mesaHeight;

        if (novaLeft < 0) novaLeft = 0;
        if (novaTop < 0) novaTop = 0;

        if (novaLeft > maxX) novaLeft = maxX;
        if (novaTop > maxY) novaTop = maxY;

        mesaSelecionada.style.left = novaLeft + "px";
        mesaSelecionada.style.top = novaTop + "px";
    }

});

document.addEventListener("mouseup", function () {

    if (mesaSelecionada) {

        mesaSelecionada.style.cursor = "grab";

        salvarPosicao(mesaSelecionada);

        mesaSelecionada = null;
    }

});

function salvarPosicao(mesa) {

    const id = mesa.getAttribute("data-id");
    const x = parseInt(mesa.style.left);
    const y = parseInt(mesa.style.top);

    console.log("Salvar:", id, x, y);
}


function salvarLayout() {

    const mesas = [];

    document.querySelectorAll(".mesa").forEach(mesa => {

        mesas.push({
            Id: parseInt(mesa.getAttribute("data-id")),
            PosicaoX: parseInt(mesa.style.left),
            PosicaoY: parseInt(mesa.style.top)
        });

    });

    fetch("https://localhost:7209/api-gastrolink/Mesa/SalvarLayout", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(mesas)
    })
        .then(res => {
            if (res.ok) {
                alert("Layout salvo com sucesso!");
            } else {
                alert("Erro ao salvar");
            }
        });

}