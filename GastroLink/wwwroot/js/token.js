export async function buscarToken() {
    const response = await fetch("/Token/ObterToken", { credentials: "include" });
    if (!response.ok) {
        throw new Error("Não foi possível obter o token");
    }
    const data = await response.json();
    return data.token;
}
//# sourceMappingURL=token.js.map