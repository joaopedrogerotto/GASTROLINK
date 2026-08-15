import { buscarToken } from "./token.js"
import { DashboardFiltro, IndicadorDashboard } from "./Models/dashboadModel.js";
declare const Chart: any;


async function gerarDashboard(filtro: DashboardFiltro): Promise<IndicadorDashboard> {
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

const canvas = document.getElementById("chart") as HTMLCanvasElement;

const ctx = canvas.getContext("2d");

let filtro: DashboardFiltro = {
    indicador: "vendas-categoria"
}

if (!ctx) {
    throw new Error("Falha ao gerar grafico");
}

const dados = await gerarDashboard(filtro)
console.log(dados);
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