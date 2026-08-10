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

export interface FormaPagamento {
    id: number;
    forma: string;
}

export interface DadosPagamento {
    pedido: Pedido;
    formasPagamento: FormaPagamento[];
}

export interface Pagamento {
    IdFormaPagamento: number;
    ValorPago: number;
}


export interface PagamentoPix {
    IdPedido: number;
    ValorPagoPix: number;
}

export interface RegistrarPagamento {
    Desconto: number;
    ValorTotal: number;
    IdPedido: number;
    IdUsuario: number;
    Pagamentos: Pagamento[];
}
