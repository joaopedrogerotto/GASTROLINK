async function criarMesa() {
    const response = await fetch('https://localhost:7209/api-gastrolink/Mesa', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            NumeroMesa: $("#numeroMesaInput").val()
        })
    });

    if (response.ok) {
        carregarMesas();
        $("#numeroMesaInput").val("");
    }
}

async function carregarMesas() {
    const response = await fetch('https://localhost:7209/api-gastrolink/Mesa');
    const mesas = await response.json();

    console.log(mesas); // 🔍 DEBUG

    const tbody = document.querySelector("#tabelaMesas tbody");
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

