using APIGastroLink.Services.Interfaces;

namespace APIGastroLink.Services {
    public class LogService : ILogService {
        private readonly ILogger<LogService> _logger;

        public LogService(ILogger<LogService> logger) {
            _logger = logger;
        }

        public void Error(Exception ex, string mensagem) {
            _logger.LogError(ex, mensagem);
        }
    }
}
