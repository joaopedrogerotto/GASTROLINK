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

export interface ItemCarrinhoChatBot {
    tipo: "carrinho";
    resposta: string;
    item: ItemPedido;
}


export interface PedidoChatbot {
    tipo: "pedido";
    resposta: string;
    numeroMesa: string;
    itens: ItemPedido[]
};

export type RecomendacaoResponse = RecomendacaoDePrato | RespostaConversa | PedidoChatbot | ItemCarrinhoChatBot | MesangemConfirmacaoPedido;

export interface MensagemHistorico {
    autor: "usuario" | "bot";
    texto: string;
}

export interface ItemPedido {
    idPrato: number;
    quantidade: number;
    observacao?: string;
    preco: number;
}

export interface Pedido {
    numeroMesa: string;
    itens: ItemPedido[];
}

export interface MesangemConfirmacaoPedido {
    tipo: "confirmacao";
    resposta: string;
}