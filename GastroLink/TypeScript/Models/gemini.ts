export interface RecomendacaoDePrato {
    tipo: "recomendacao";
    prato_recomendado: string;
    motivo: string;
    destaques: string[];
    harmonizacao: string;
}

export interface RespostaConversa {
    tipo: "conversa";
    resposta: string;
}

export type RecomendacaoResponse = RecomendacaoDePrato | RespostaConversa;

export interface GeminiApiResponse {
    candidates: {
        content: {
            parts: { text: string }[];
        }
    }[];
    error?: {
        message: string;
        code: number;
    };
}

export interface MensagemHistorico {
    autor: "usuario" | "bot";
    texto: string;
}
