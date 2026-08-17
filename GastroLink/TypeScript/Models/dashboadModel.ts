export interface DashboardFiltro {
    indicador: string;
    dataInicio?: Date;
    dataFim?: Date;
}

export interface DadoDashboard {
    label: string;
    valor: number;
    data?: Date;
}

export interface IndicadorDashboard {
    nome: string;
    tipo: string;
    dados: DadoDashboard[];
}