"use strict";
async function criarMesa() {
    const numeroMesa = $("#numeroMesaInput").val();
    const response = await fetch('/Mesa/SalvarMesa', {
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
async function carregarMesas() {
    const response = await fetch('/Mesa/TodasMesasJson');
    const mesas = await response.json();
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
//# sourceMappingURL=mesa.js.map