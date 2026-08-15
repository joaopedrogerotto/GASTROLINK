import { buscarToken } from "../token.js";
export class PedidoHub {
    constructor(hubNome) {
        this.connection = new signalR.HubConnectionBuilder().withUrl(`${window.APP_CONFIG.apiSignalR}/${hubNome}`, { accessTokenFactory: () => buscarToken() }).withAutomaticReconnect().build();
    }
    async startConnection() {
        try {
            await this.connection.start();
        }
        catch (error) {
            console.error("Erro de coneão com o SignalR:", error);
            setTimeout(() => this.startConnection(), 5000);
        }
    }
    onNovoPedido(callback) {
        this.connection.on("NovoPedido", callback);
    }
    onPedidoPronto(callback) {
        this.connection.on("PedidoPronto", callback);
    }
    onPedidoAguarndandoPag(callback) {
        this.connection.on("AguardandoPagamento", callback);
    }
}
//# sourceMappingURL=pedidoHub.js.map