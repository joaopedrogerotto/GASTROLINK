interface StatusMesa {
    status: string;
}

interface Mesa {
    numero: number;
    status: StatusMesa;
}


async function criarMesa(): Promise<void> {
    const numeroMesa = $("#numeroMesaInput").val() as string;

    const response = await fetch('https://localhost:7209/api-gastrolink/Mesa', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            NumeroMesa: numeroMesa
        })
    });

    if (response.ok) {
        carregarMesas();
        $("#numeroMesaInput").val("");
    }
}

async function carregarMesas(): Promise<void> {
    const response = await fetch('https://localhost:7209/api-gastrolink/Mesa');
    const mesas: Mesa[] = await response.json();

    const tbody = document.querySelector("#tabelaMesas tbody") as HTMLTableSectionElement;
    tbody.innerHTML = "";

    mesas.forEach(mesa => {
        const linha = `
            <tr>
                <td>${mesa.numero}</td>
                <td>${mesa.status.status}</td>
            </tr>
        `;

        tbody.innerHTML += linha;
    });
}

document.addEventListener("DOMContentLoaded", () => {
    carregarMesas();
});

