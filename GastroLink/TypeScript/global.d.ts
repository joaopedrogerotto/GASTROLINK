declare const bootstrap: any;
declare const signalR: any;

interface Window {
    APP_CONFIG: {
        apiBaseUrl: string;
        apiSignalR: string;
        apiDashboard: string;
        apiGemini: string;
    };
}