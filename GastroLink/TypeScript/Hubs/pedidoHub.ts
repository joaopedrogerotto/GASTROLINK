async function buscarToken(): Promise<string> {
    const response = await fetch("/Token/ObterToken", { credentials: "include" });
    if (!response.ok) {
        throw new Error("Não foi possível obter o token");
    }
    const data = await response.json();
    return data.token;
}


export class PedidoHub {
    private connection: any;

    constructor(hubNome: string) {
        this.connection = new signalR.HubConnectionBuilder().withUrl(`${window.APP_CONFIG.apiSignalR}/${hubNome}`, { accessTokenFactory: () => buscarToken() }).withAutomaticReconnect().build();
    }

    async startConnection(): Promise<void> {
         try {
            await this.connection.start();
        } catch (error) {
            console.error("Erro de coneão com o SignalR:", error);
            setTimeout(() => this.startConnection(), 5000);
        }
    }

    onNovoPedido(callback: (pedido: any) => void): void {
        this.connection.on("NovoPedido", callback);
    }

    onPedidoPronto(callback: (pedido: any) => void): void {
        this.connection.on("PedidoPronto", callback);
    }

    onPedidoAguarndandoPag(callback: (pedido: any) => void): void {
        this.connection.on("AguardandoPagamento", callback);
    }
}