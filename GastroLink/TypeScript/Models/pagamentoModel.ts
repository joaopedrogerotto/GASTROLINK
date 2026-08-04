export interface Prato {
    nome: string;
}

export interface ItemPedido {
    quantidade: number;
    observacao: string | null;
    prato: Prato;
}

export interface Pedido {
    id: number;
    mesa: {
        numero: number;
    };
    usuario: {
        nome: string;
    };
    itens: ItemPedido[];
    valorTotal: number;
    dataCriacao: Date;
}

export interface StatusPedido {
    IdPedido: Number,
    IdStatusPedido: Number
}

export interface FormaPagamento {
    id: number;
    forma: string;
}

export interface DadosPagamento {
    pedido: Pedido;
    formasPagamento: FormaPagamento[];
}

export interface Pagamento {
    Desconto: number;
    ValorPago: number;
    ValorTotal: number;
    IdPedido: number;
    IdFormaPagamento: number;
    IdUsuario: number;
}
