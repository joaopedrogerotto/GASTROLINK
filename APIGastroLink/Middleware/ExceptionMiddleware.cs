using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Middleware {
    public class ExceptionMiddleware {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context, ILogService logService) {
            try {
                await _next(context);
            } catch (Exception ex) {
                logService.Error(ex, $"Erro não tratado em {context.Request.Method} {context.Request.Path}");
                context.Response.StatusCode = 500;

                await context.Response.WriteAsJsonAsync(new {
                    sucesso = false,
                    mensagem = "Ocorreu um erro interno no servidor."
                });
            }
        }
    }
}
