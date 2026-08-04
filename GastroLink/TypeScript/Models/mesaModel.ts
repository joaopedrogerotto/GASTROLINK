export interface StatusMesa {
    status: string;
}

export interface Mesa {
    numero: number;
    status: StatusMesa;
}

export interface MesaLayout {
    Id: number;
    PosicaoX: number;
    PosicaoY: number;
}