import { MesaLayout } from "./Models/mesaModel.js";

let mesaSelecionada: HTMLElement | null = null;
let offsetX: number = 0;
let offsetY: number = 0;

const mapa = document.getElementById("mapa") as HTMLElement;


document.querySelectorAll<HTMLElement>(".mesa").forEach(mesa => {

    mesa.addEventListener("mousedown", function (e: MouseEvent) {

        mesaSelecionada = mesa;

        const rect = mesa.getBoundingClientRect();

        offsetX = e.clientX - rect.left;
        offsetY = e.clientY - rect.top;

        mesa.style.cursor = "grabbing";
    });

});


document.addEventListener("mousemove", function (e: MouseEvent) {

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

function salvarPosicao(mesa: HTMLElement): void {
    const id: number = parseInt(mesa.getAttribute("data-id") || "0");
    const x: number = parseInt(mesa.style.left);
    const y: number = parseInt(mesa.style.top);
}


function salvarLayout(): void {

    const mesas: MesaLayout[] = [];

    document.querySelectorAll<HTMLElement>(".mesa").forEach(mesa => {
        mesas.push({
            Id: parseInt(mesa.getAttribute("data-id") || "0"),
            PosicaoX: parseInt(mesa.style.left),
            PosicaoY: parseInt(mesa.style.top)
        });

    });

    fetch("/Mesa/SalvarLayoutMesas", {
        method: "POST",
        headers: {
            "Content-Type": "application/json"
        },
        body: JSON.stringify(mesas)
    }).then((res: Response) => {
        if (res.ok) {
            mostrarModal("success");
        } else {
            mostrarModal("error");
        }
    });
}

function mostrarModal(status: string): void {
    const header = $("#headerSalvarLayout");

    if (status == "success") {
        header.removeClass("bg-danger text-white");
        header.addClass("bg-success text-white");

        $("#statusSalvarLayout").text("Layout salvo com sucesso.");
    }
    else {
        header.removeClass("bg-success text-white");
        header.addClass("bg-danger text-white");

        $("#statusSalvarLayout").text("Erro ao salvar layout.");
    }

    bootstrap.Modal.getOrCreateInstance(document.getElementById("modalSalvarLayout")).show();
}

document.addEventListener("click", (e) => {
    const mesa = (e.target as HTMLElement).closest(".mesa-livre");

    if (!mesa) return;

    window.location.href = `/Pedido/CriarPedido?idMesa=${mesa.getAttribute("data-id")}`;
})