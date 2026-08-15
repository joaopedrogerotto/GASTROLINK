export async function buscarToken(): Promise<string> {
    const response = await fetch("/Token/ObterToken", { credentials: "include" });
    if (!response.ok) {
        throw new Error("Não foi possível obter o token");
    }
    const data = await response.json();
    return data.token;
}
