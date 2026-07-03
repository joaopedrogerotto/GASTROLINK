interface StatusMesa {
    status: string;
}

interface Mesa {
    numero: number;
    status: StatusMesa;
}

async function criarMesa(): Promise<void> {
    const numeroMesa = $("#numeroMesaInput").val() as string;

    const response = await fetch('/Mesa/SalvarMesa', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            NumeroMesa: numeroMesa
        })
    });

    const header = $("#headerModalCadastroMesa");
    if (response.ok) {
        header.removeClass("bg-danger text-white");
        header.addClass("bg-success text-white");
        $("#statusCadastroMesa").text("Mesa criada com sucesso.");
        bootstrap.Modal.getOrCreateInstance(document.getElementById("modalCadastroMesa")).show();
        carregarMesas();
    } else {
        header.removeClass("bg-success text-white");
        header.addClass("bg-danger text-white");
        if (response.status === 409) {
            $("#statusCadastroMesa").text("Mesa já cadastrada.");
        } else {
            const erro = await response.json();
            $("#statusCadastroMesa").text(erro.message ?? erro.Message ?? "Erro desconhecido.");
        }
        bootstrap.Modal.getOrCreateInstance(document.getElementById("modalCadastroMesa")).show();
    }
    $("#numeroMesaInput").val("");
}

async function carregarMesas(): Promise<void> {
    const response = await fetch('/Mesa/TodasMesasJson');
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

