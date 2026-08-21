import { GeminiApiResponse } from "./Models/geminiModel.js";
import { Prato } from "./Models/pratoModel.js"
import { RecomendacaoResponse, MensagemHistorico, ItemPedido, Pedido } from "./Models/chatbotModel.js"

const GEMINI_API_KEY = window.APP_CONFIG.apiGemini;
const GEMINI_MODEL = "gemini-3.5-flash-lite";
const GEMINI_URL = `https://generativelanguage.googleapis.com/v1beta/models/${GEMINI_MODEL}:generateContent`;

let historico: MensagemHistorico[] =[];
let carrinho: ItemPedido[] = [];
export async function recomendarPrato(pratos: Prato[],textoUsuario: string,historico: MensagemHistorico[]): Promise<RecomendacaoResponse> {
    const historicoTexto = historico.map(m => `${m.autor === "usuario" ? "Usuário" : "Assistente"}: ${m.texto}`).join("\n");
    const prompt = `
    Você é um assistente de atendimento de um restaurante, simpático e prestativo.

    Lista de pratos disponíveis (JSON):
    ${JSON.stringify(pratos)}

    Histórico da conversa até agora:
    ${historicoTexto || "(início da conversa)"}

    Nova mensagem do usuário: "${textoUsuario}"

    Leve em conta o histórico para entender o contexto. Por exemplo, se você perguntou algo e o usuário respondeu "sim" ou algo curto, isso se refere à sua última pergunta.

    O usuário também pode pedir para que o prato que você recomendou seja finalizado como pedido. Caso seja solicitado tal, usar o modelo de pedido no final do prompt.

    Primeiro, avalie a intenção da mensagem:
    - Se o usuário está pedindo uma recomendação (diretamente ou confirmando que quer uma sugestão que você ofereceu), use o tipo "recomendacao".
    - Se for pergunta geral ou conversa, use o tipo "conversa".
    - No tipo recomendação, não precisa sempre recomendar uma bebida ou acompanhamento, só caso você veja que é necessário. Pois não se faz necessário bebiba para doce na concepção do negócio.
    - Atenção: o usuario pode querer adicionar mais de um item no seu pedido, com base na leitura do histórico adicione todos no array de itens. Faça esse loop de sempre perguntar se o usuario que adicionar mais algum item até ele dizer ao contrario (Ou seja, que deseja finalizar o pedido ou coisas semelhantes dependendo do contexto)
    - Quando for para FINALIZAR o pedido, você deverá ler o carrinho: ${carrinho}. Se não tiver nenhum item, informar ao usuario.

    Retorne APENAS um JSON válido, sem markdown, em um dos formatos:

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

    Se for finalizar pedido, retorne o pedido já no formato abaixo, usando o idPrato e preco EXATOS da lista de pratos fornecida (nunca invente ou aproxime valores). Reforço ainda que todos o itens é um array onde pode ter mais de um prazo:
        {
          "tipo": "pedido",
          "resposta": "<confirmação natural pro usuário, ex: 'Perfeito, irei finalizar seu pedido e enviar para a cozinha'>",
          "idMesa": 1,
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

    const data: GeminiApiResponse = await response.json();

    if (!response.ok) {
        throw new Error(`Erro na API Gemini: ${data.error?.message ?? "desconhecido"}`);
    }

    const textoResposta = data.candidates?.[0]?.content?.parts?.[0]?.text;
    if (!textoResposta) throw new Error("Resposta da API veio vazia");

    return JSON.parse(textoResposta) as RecomendacaoResponse;
}

async function selecionarTodosPratos(): Promise<Prato[]> {
    const response = await fetch("/Chatbot/TodosPratoChatbot");

    if (!response.ok) {
        throw new Error(`Erro ao buscar pratos: ${response.status}`);
    }

    return await response.json();
}

const chatBox = document.getElementById("chat-box") as HTMLDivElement;
const input = document.getElementById("chat-input") as HTMLInputElement;
const form = document.getElementById("chat-form") as HTMLFormElement;

let pratos: Prato[] = [];

function adicionarMensagem(texto: string, autor: "usuario" | "bot") {
    const msg = document.createElement("div");
    msg.className =
        autor === "usuario"
            ? "align-self-end bg-primary text-white rounded-3 px-3 py-2"
            : "align-self-start bg-light text-dark rounded-3 px-3 py-2 border";
    msg.style.maxWidth = "80%";
    msg.style.whiteSpace = "pre-line"; // <-- adiciona isso
    msg.textContent = texto;
    chatBox.appendChild(msg);
    chatBox.scrollTop = chatBox.scrollHeight;
}

function adicionarCarregando(): HTMLDivElement {
    const loading = document.createElement("div");
    loading.className =
        "align-self-start bg-light text-muted fst-italic rounded-3 px-3 py-2 border d-flex align-items-center gap-2";
    loading.style.maxWidth = "80%";
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
    } catch (error) {
        console.error("Erro ao carregar pratos:", error);
        adicionarMensagem(
            "Não foi possível carregar o cardápio. Recarregue a página.",
            "bot"
        );
        return; 
    }

    input.disabled = false;
    input.focus();
}

iniciarChat();

form.addEventListener("submit", async (event) => {
    event.preventDefault();

    const texto = input.value.trim();
    if (!texto) return;

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
            adicionarMensagem(
                `🍽️ Recomendo: ${resultado.prato_recomendado}\n\n${resultado.motivo}\n\n${destaquesTexto}\n\n🥂 ${resultado.harmonizacao}`,
                "bot"
            );
            historico.push({ autor: "bot", texto: `Recomendei o prato "${resultado.prato_recomendado}".` });
        } else if (resultado.tipo === "pedido") {
            console.log(resultado);

            const idValidos = new Set(pratos.map(p => p.id));
            const itensValidos = resultado.itens.filter(i => !idValidos.has(i.idPrato));

            if (itensValidos.length > 0) {
                const msg = "Não consegui confirmar um dos itens do pedido, pode tentar de novo?";
                adicionarMensagem(msg, "bot");
                historico.push({ autor: "bot", texto: msg });
            } else {
                adicionarMensagem(resultado.resposta, "bot");
                historico.push({ autor: "bot", texto: resultado.resposta });
                await finalizarPedido(resultado.idMesa, resultado.itens);
            }
        } else if (resultado.tipo == "carrinho") {
            const idValido = pratos.some(p => p.id === resultado.item.idPrato);
            if (!idValido) {
                const msg = "Não consegui confirmar um dos itens do pedido, pode tentar de novo?";
                adicionarMensagem(msg, "bot");
                historico.push({ autor: "bot", texto: msg });
            } else {
                adicionarMensagem(resultado.resposta, "bot");
                historico.push({ autor: "bot", texto: resultado.resposta });
                adicionarItemCarrinho(resultado.item);
            }
        } else {
            adicionarMensagem(resultado.resposta, "bot");
            historico.push({ autor: "bot", texto: resultado.resposta });
        }
    } catch (error) {
        loadingEl.remove();
        const mensagemErro = error instanceof Error ? error.message : "Erro desconhecido";
        adicionarMensagem(`Erro: ${mensagemErro}`, "bot");
        console.error(error);
    } finally {
        input.disabled = false;
        input.focus();
    }
});

async function finalizarPedido(idMesa: number, itens: ItemPedido[]): Promise<void> {
    const pedidoPayload: Pedido = { idMesa, itens };

    const response = await fetch("/Pedido/GerarPedidoChatbot", {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify(pedidoPayload),
    })

    if (!response.ok) {
        adicionarMensagem(`Erro ao finalizar pedido: ${response.status}`, "bot");
    } else {
        adicionarMensagem("Pedido criado com sucesso", "bot");
    }
}

function adicionarItemCarrinho(item: ItemPedido) {
    const itemExistente = carrinho.find(
        i => i.idPrato === item.idPrato
    );

    if (itemExistente) {
        itemExistente.quantidade += item.quantidade;
    } else {
        carrinho.push(item);
    }
}