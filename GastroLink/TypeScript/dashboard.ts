import { buscarToken } from "./token.js"
import { DashboardFiltro, IndicadorDashboard } from "./Models/dashboadModel.js";
declare const Chart: any;


async function gerarIndicadores(filtro: DashboardFiltro): Promise<IndicadorDashboard> {
    const token = await buscarToken();

    const response = await fetch(`${window.APP_CONFIG.apiDashboard}/Dashboard`, {
        method: "POST",
        headers: {
            "Content-Type": "application/json",
            "Authorization": `Bearer ${token}`
        },
        body: JSON.stringify(filtro)
    });

    if (!response.ok) {
        throw new Error("Falha:" + response.json());
    }

    return await response.json();
}


async function gerarDashboard() {
    const canvas = document.getElementById("chart") as HTMLCanvasElement;

    const ctx = canvas.getContext("2d");


    let filtro: DashboardFiltro = {
        indicador: (document.getElementById("selectDashboard") as HTMLSelectElement).value,
        dataInicio: new Date((document.getElementById("dataInicio") as HTMLInputElement).value),
        dataFim: new Date((document.getElementById("dataFim") as HTMLInputElement).value)
    }

    if (!ctx) {
        throw new Error("Falha ao gerar grafico");
    }

    const graficoExistente = Chart.getChart("chart");

    if (graficoExistente) {
        graficoExistente.destroy();
    }

    const dados = await gerarIndicadores(filtro)
    if (dados.tipo === "line") {
        const datas = [...new Set(dados.dados.map(x => x.data))];
        const mapLabel = [... new Set(dados.dados.map(x => x.label))];

        const datasets = mapLabel.map(label => ({
            label: label,
            data: datas.map(data => {
                const registro = dados.dados.find(x => x.data === data && x.label === label);
                return registro?.valor ?? 0;
            })
        }));

        new Chart(ctx, {
            type: dados.tipo,
            data: {
                labels: datas.map(data => data? new Date(data).toLocaleDateString("pt-BR") : ""),
                datasets: datasets
            }
        })
    } else {
        new Chart(ctx, {
            type: dados.tipo,
            data: {
                labels: dados.dados.map(x => x.label),
                datasets: [
                    {
                        label: dados.nome,
                        data: dados.dados.map(x => x.valor)
                    }
                ]
            }
        })
    }
}

document.getElementById("gerarDashboard")?.addEventListener("click", function () {
    gerarDashboard();
});