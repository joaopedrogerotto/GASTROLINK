const GEMINI_API_KEY = window.APP_CONFIG.apiGemini;
const GEMINI_MODEL = "gemini-3.5-flash-lite";
const GEMINI_URL = `https://generativelanguage.googleapis.com/v1beta/models/${GEMINI_MODEL}:generateContent`;
let historico = [];
export async function recomendarPrato(pratos, textoUsuario, historico) {
    const historicoTexto = historico.map(m => `${m.autor === "usuario" ? "Usuário" : "Assistente"}: ${m.texto}`).join("\n");
    const prompt = `
    Você é um assistente de atendimento de um restaurante, simpático e prestativo.

    Lista de pratos disponíveis (JSON):
    ${JSON.stringify(pratos)}

    Histórico da conversa até agora:
    ${historicoTexto || "(início da conversa)"}

    Nova mensagem do usuário: "${textoUsuario}"

    Leve em conta o histórico para entender o contexto. Por exemplo, se você perguntou algo e o usuário respondeu "sim" ou algo curto, isso se refere à sua última pergunta.

    Primeiro, avalie a intenção da mensagem:
    - Se o usuário está pedindo uma recomendação (diretamente ou confirmando que quer uma sugestão que você ofereceu), use o tipo "recomendacao".
    - Se for pergunta geral ou conversa, use o tipo "conversa".
    - No tipo recomendação, não precisa sempre recomendar uma bebida ou acompanhamento, só caso você veja que é necessário. Pois não se faz necessário bebiba para doce na concepção do negócio.

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
function adicionarCarregando() {
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
        }
        else {
            adicionarMensagem(resultado.resposta, "bot");
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
//# sourceMappingURL=chatbot.js.map