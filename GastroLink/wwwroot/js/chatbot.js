const GEMINI_API_KEY = window.APP_CONFIG.apiGemini;
const GEMINI_MODEL = "gemini-3.5-flash-lite";
const GEMINI_URL = `https://generativelanguage.googleapis.com/v1beta/models/${GEMINI_MODEL}:generateContent`;
let historico = [];
let carrinho = [];
let aguardandoConfirmacao = false;
export async function recomendarPrato(pratos, textoUsuario, historico) {
    const historicoTexto = historico.map(m => `${m.autor === "usuario" ? "Usuário" : "Assistente"}: ${m.texto}`).join("\n");
    const prompt = `
    Você é um assistente de atendimento de um restaurante, simpático e prestativo.

    Lista de pratos disponíveis (JSON):
    ${JSON.stringify(pratos)}

    Histórico da conversa até agora:
    ${historicoTexto || "(início da conversa)"}

    Nova mensagem do usuário: "${textoUsuario}"

    Carrinho atual do usuário (JSON):
    ${JSON.stringify(carrinho)}

    Leve em conta o histórico para entender o contexto. Se você perguntou algo e o usuário respondeu "sim" ou algo curto, isso se refere à sua última pergunta.

    ### Como identificar a intenção da mensagem

    - Recomendação: o usuário pede uma sugestão de prato, diretamente ou confirmando que quer uma sugestão que você ofereceu → tipo "recomendacao".
      - Só inclua bebida/acompanhamento em "harmonizacao" quando fizer sentido (ex: não sugerir bebida para doce).
    - Adicionar item ao pedido: o usuário confirma que quer pedir um prato específico → tipo "carrinho".
      - O usuário pode querer adicionar vários itens ao longo da conversa; use o histórico para saber o que já foi confirmado.
      - Depois de adicionar um item, sempre pergunte se ele quer incluir mais alguma coisa, até que ele diga que quer finalizar o pedido.
    - Pergunta geral ou conversa que não se encaixa nos outros casos → tipo "conversa".

    ### Fluxo de finalização do pedido (siga esta ordem, sem pular etapas)

    Esse fluxo só começa quando o usuário sinaliza que quer finalizar o pedido (ex: "finalizar", "fechar a conta", "só isso mesmo").

    1. **Carrinho vazio**: se o carrinho estiver vazio, informe isso ao usuário (tipo "conversa") e não avance no fluxo.
    2. **Número da mesa**: se ainda não souber o número da mesa, pergunte antes de qualquer outra coisa (tipo "conversa").
    3. **Confirmação obrigatória**: assim que você tiver carrinho + número da mesa, SEMPRE retorne o tipo "confirmacao" com uma frase curta pedindo para o usuário revisar o pedido. NÃO liste os itens, quantidades ou preços na resposta — a lista será exibida separadamente pelo sistema.
    4. **Finalização**: só retorne o tipo "pedido" na mensagem seguinte, e apenas se a última mensagem do usuário for uma resposta afirmativa explícita (ex: "sim", "confirmo", "pode finalizar") à pergunta de confirmação feita no passo anterior. Use os valores de idPrato e preco EXATOS da lista de pratos fornecida (nunca invente ou aproxime valores). "itens" é sempre um array, podendo conter mais de um prato.

    Nunca combine os passos 2, 3 e 4 na mesma resposta. Cada um deve ser uma troca de mensagem separada com o usuário.

    Retorne APENAS um JSON válido, sem markdown, em um dos formatos abaixo:

    Se for recomendação:
    {
      "tipo": "recomendacao",
      "prato_recomendado": "<nome exato do prato, igual ao da lista>",
      "motivo": "<explicação detalhada de 3 a 5 frases>",
      "destaques": ["<característica 1>", "<característica 2>", "<característica 3>"],
      "harmonizacao": "<sugestão de bebida ou acompanhamento>"
    }

    Se for conversa:
    {
      "tipo": "conversa",
      "resposta": "<resposta natural, considerando o histórico>"
    }

    Se for para adicionar o item ao carrinho/pedido:{
        "tipo":"carrinho",
        "resposta": "<confirmação natural pro usuário, ex: 'Show! Adicionei o X ao seu pedido.'>",
        "item": {
              "idPrato": <id exato do prato, igual ao da lista>,
              "quantidade": <número, padrão 1 se não informado>,
              "observacao": "<opcional, null se não informado>",
              "preco": <preço unitário do prato multiplicado pela quantidade>
            } 
    }

    Se for confirmação antes de finalizar:{
        "tipo":"confirmacao",
        "resposta": "<frase curta e natural convidando a revisar o pedido, SEM listar os itens nem valores, ex: 'Antes de finalizar, poderia confirmar os itens do seu pedido abaixo?'>",
    }

    Se for finalizar pedido após confirmação do usuario, retorne o pedido já no formato abaixo, usando o idPrato e preco EXATOS da lista de pratos fornecida (nunca invente ou aproxime valores). Reforço ainda que todos o itens é um array onde pode ter mais de um prazo:
        {
          "tipo": "pedido",
          "resposta": "<confirmação natural pro usuário, ex: 'Perfeito, irei finalizar seu pedido e enviar para a cozinha'>",
          "numeroMesa": <de acordo com o que o usuário informar> ,
          "itens": [
            {
              "idPrato": <id exato do prato, igual ao da lista>,
              "quantidade": <número, padrão 1 se não informado>,
              "observacao": "<opcional, null se não informado>",
              "preco": <preço unitário do prato multiplicado pela quantidade>
            } 
          ]
        }
    
    `;
    const response = await fetch(`${GEMINI_URL}?key=${GEMINI_API_KEY}`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({
            contents: [{ parts: [{ text: prompt }] }],
            generationConfig: { temperature: 0.9, responseMimeType: "application/json" },
        }),
    });
    const data = await response.json();
    if (!response.ok) {
        throw new Error(`Erro na API Gemini: ${data.error?.message ?? "desconhecido"}`);
    }
    const textoResposta = data.candidates?.[0]?.content?.parts?.[0]?.text;
    if (!textoResposta)
        throw new Error("Resposta da API veio vazia");
    return JSON.parse(textoResposta);
}
async function selecionarTodosPratos() {
    const response = await fetch("/Chatbot/TodosPratoChatbot");
    if (!response.ok) {
        throw new Error(`Erro ao buscar pratos: ${response.status}`);
    }
    return await response.json();
}
const chatBox = document.getElementById("chat-box");
const input = document.getElementById("chat-input");
const form = document.getElementById("chat-form");
let pratos = [];
function adicionarMensagem(texto, autor) {
    const msg = document.createElement("div");
    msg.className = autor === "usuario" ? "msg-usuario" : "msg-bot";
    msg.textContent = texto;
    chatBox.appendChild(msg);
    chatBox.scrollTop = chatBox.scrollHeight;
}
function adicionarCarregando() {
    const loading = document.createElement("div");
    loading.className = "msg-bot msg-carregando";
    loading.innerHTML = `<span class="spinner-border spinner-border-sm"></span> Pensando...`;
    chatBox.appendChild(loading);
    chatBox.scrollTop = chatBox.scrollHeight;
    return loading;
}
async function iniciarChat() {
    input.disabled = true;
    input.placeholder = "Carregando cardápio...";
    try {
        pratos = await selecionarTodosPratos();
        input.placeholder = "Ex: quero algo leve...";
    }
    catch (error) {
        console.error("Erro ao carregar pratos:", error);
        adicionarMensagem("Não foi possível carregar o cardápio. Recarregue a página.", "bot");
        return;
    }
    input.disabled = false;
    input.focus();
}
iniciarChat();
form.addEventListener("submit", async (event) => {
    event.preventDefault();
    const texto = input.value.trim();
    if (!texto)
        return;
    adicionarMensagem(texto, "usuario");
    historico.push({ autor: "usuario", texto });
    input.value = "";
    input.disabled = true;
    const loadingEl = adicionarCarregando();
    try {
        const resultado = await recomendarPrato(pratos, texto, historico);
        loadingEl.remove();
        if (resultado.tipo === "recomendacao") {
            const destaquesTexto = resultado.destaques.map(d => `• ${d}`).join("\n");
            adicionarMensagem(`🍽️ Recomendo: ${resultado.prato_recomendado}\n\n${resultado.motivo}\n\n${destaquesTexto}\n\n🥂 ${resultado.harmonizacao}`, "bot");
            historico.push({ autor: "bot", texto: `Recomendei o prato "${resultado.prato_recomendado}".` });
        }
        else if (resultado.tipo === "pedido") {
            if (!aguardandoConfirmacao) {
                let msg = "Antes de finaliaar, preciso confimar o seu pedido";
                adicionarMensagem(msg, "bot");
                historico.push({ autor: "bot", texto: msg });
            }
            aguardandoConfirmacao = false;
            const idValidos = new Set(pratos.map(p => p.id));
            const itensValidos = resultado.itens.filter(i => !idValidos.has(i.idPrato));
            if (itensValidos.length > 0) {
                const msg = "Não consegui confirmar um dos itens do pedido, pode tentar de novo?";
                adicionarMensagem(msg, "bot");
                historico.push({ autor: "bot", texto: msg });
            }
            else {
                adicionarMensagem(resultado.resposta, "bot");
                historico.push({ autor: "bot", texto: resultado.resposta });
                await finalizarPedido(resultado.numeroMesa, resultado.itens);
            }
        }
        else if (resultado.tipo == "carrinho") {
            const idValido = pratos.some(p => p.id === resultado.item.idPrato);
            if (!idValido) {
                const msg = "Não consegui confirmar um dos itens do pedido, pode tentar de novo?";
                adicionarMensagem(msg, "bot");
                historico.push({ autor: "bot", texto: msg });
            }
            else {
                adicionarMensagem(resultado.resposta, "bot");
                historico.push({ autor: "bot", texto: resultado.resposta });
                adicionarItemCarrinho(resultado.item);
            }
        }
        else if (resultado.tipo === "confirmacao") {
            aguardandoConfirmacao = true;
            resultado.resposta += mensagemConfirmacaoPedido();
            adicionarMensagem(resultado.resposta, "bot");
            historico.push({ autor: "bot", texto: resultado.resposta });
        }
        else {
            adicionarMensagem(resultado.resposta, "bot");
            historico.push({ autor: "bot", texto: resultado.resposta });
        }
    }
    catch (error) {
        loadingEl.remove();
        const mensagemErro = error instanceof Error ? error.message : "Erro desconhecido";
        adicionarMensagem(`Erro: ${mensagemErro}`, "bot");
        console.error(error);
    }
    finally {
        input.disabled = false;
        input.focus();
    }
});
async function finalizarPedido(numeroMesa, itens) {
    const pedidoPayload = { numeroMesa: numeroMesa.toString(), itens: itens };
    const response = await fetch("/Pedido/GerarPedidoChatbot", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(pedidoPayload),
    });
    if (!response.ok) {
        adicionarMensagem(`Erro ao finalizar pedido: ${response.status}`, "bot");
    }
    else {
        adicionarMensagem("Pedido criado com sucesso", "bot");
    }
}
function mensagemConfirmacaoPedido() {
    const itens = carrinho.map(item => {
        const prato = pratos.find(p => p.id === item.idPrato);
        const nome = prato ? prato.nome : "Item desconhecido";
        return `${item.quantidade}x ${nome}`;
    }).join("\n");
    return `\n\n${itens}\n\nDeseja confirmar?`;
}
function adicionarItemCarrinho(item) {
    const itemExistente = carrinho.find(i => i.idPrato === item.idPrato);
    if (itemExistente) {
        itemExistente.quantidade += item.quantidade;
    }
    else {
        carrinho.push(item);
    }
}
//# sourceMappingURL=chatbot.js.map