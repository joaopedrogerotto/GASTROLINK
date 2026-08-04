export interface RascunhoItemPedido {
    Prato: Prato;
    Observacao: string | null;
    Quantidade: number;
}

export interface AdicionarItemRascunhoDTO {
    mesaId: number;
    rascunhoItemPedido: RascunhoItemPedido;
}

export interface Prato {
    Id: number;
}